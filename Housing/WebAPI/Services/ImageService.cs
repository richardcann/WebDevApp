using AutoMapper;
using Housing.WebAPI.Models;
using Housing.WebAPI.Models.InternalDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.WebAPI.Services
{
    public class ImageService
    {
        private static string diskPrefix = "c:\\temp\\";

        private IMapper _mapper;
        private readonly HousingContext _context;

        public ImageService(
            IMapper mapper,
            HousingContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /*
         * 
         * Methods used to handle DB references to images
         * 
         */

        public async Task Add(Image im)
        {
            _context.Image.Add(im);
            await _context.SaveChangesAsync();
        }

        public async void Remove(Image i)
        {
            _context.Image.Remove(i);
            await _context.SaveChangesAsync();
        }

        public List<Image> GetByProperty(int propertyRef)
        {
            return _context.Image.Where(i => i.PropertyRef == propertyRef).ToList();
        }

        public bool Update(int propertyRef, List<string> newImages)
        {
            List<Image> oldImages = GetByProperty(propertyRef);
            List<BasicImage> oldBasicImages = _mapper.Map<List<Image>, List<BasicImage>>(oldImages);

            CleanImagesFromDisk(oldBasicImages);

            //Remove all images from DB
            foreach (Image i in oldImages)
                Remove(i);
            
            List<BasicImage> newBIs = WriteImagesToDisk(newImages);

            if(newBIs == null)
            {
                return false;
            } else
            {
                foreach (BasicImage bi in newBIs)
                {
                    bi.PropertyRef = propertyRef;
                    Image im = _mapper.Map<BasicImage, Image>(bi);
                    Add(im);
                }
                return true;
            }
        }

        public bool DeleteAll(int id)
        {
            List<Image> oldImages = GetByProperty(id);
            List<BasicImage> oldBasicImages = _mapper.Map<List<Image>, List<BasicImage>>(oldImages);

            CleanImagesFromDisk(oldBasicImages);

            //Remove all images from DB
            foreach (Image i in oldImages)
                Remove(i);

            return true;
        }

        public bool AddAll(int id, List<string> b64Images)
        {
            List<BasicImage> bis = WriteImagesToDisk(b64Images);
            if(bis == null)
            {
                return false;
            }

            foreach (BasicImage bi in bis)
            {
                bi.PropertyRef = id;
                Image im = _mapper.Map<BasicImage, Image>(bi);
                Add(im);
            }
            return true;

        }

        
        
        /*
         * 
         * Methods used to write/delete images to/from disk
         * 
         */

        private static void CleanImagesFromDisk(List<BasicImage> images)
        {
            foreach (BasicImage bi in images)
            {
                string filePath = diskPrefix + bi.Path;
                System.IO.File.Delete(filePath);
            }
        }

        public List<BasicImage> WriteImagesToDisk(List<string> base64Images)
        {
            List<BasicImage> images = new List<BasicImage>();
            int imgCount = 0;
            foreach (string imageStr in base64Images)
            {
                BasicImage bi = WriteImageToDisk(imageStr, imgCount);

                if (bi != null)
                {
                    images.Add(bi);
                    imgCount++;
                }
                else
                {
                    CleanImagesFromDisk(images);
                    return null;
                }
            }
            return images;
        }

        private static BasicImage WriteImageToDisk(string imageStr, int imgCount)
        {
            //Block injection attempt
            if (imageStr.IndexOf("data:image/") != 0)
            {
                return null;
            }

            int pStart = imageStr.IndexOf("data:image/") + "data:image/".Length;
            int pEnd = imageStr.IndexOf(";");
            string mime = imageStr.Substring(pStart, pEnd - pStart);

            //Only allowing these types of files to be uploaded
            //otherwise completely reject
            //image/jpeg
            //image/png

            if (mime == "jpeg" || mime == "png")
            {
                string b64Content = imageStr.Substring(pStart + mime.Length + ";base64,".Length);
                byte[] imageBytes = Convert.FromBase64String(b64Content);
                string fileext = "." + mime;
                string guid = Guid.NewGuid().ToString();
                string pathUID = guid + fileext;

                System.IO.File.WriteAllBytes(diskPrefix + pathUID, imageBytes);

                BasicImage bi = new BasicImage
                {
                    //Initialise as -1 prior to autogeneration of PropertyID image is bound to
                    PropertyRef = -1,
                    Position = ++imgCount,
                    Path = pathUID
                };

                return bi;
            }
            else
            {
                return null;
            }
        }
    }
}
