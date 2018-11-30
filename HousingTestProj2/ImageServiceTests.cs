using AutoMapper;
using Housing.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Helpers;
using Xunit;
using System.Linq;
using Housing.WebAPI.Services;
using System.Threading;

namespace HousingTestProj2
{
    public class ImageServiceTests
    {
        private static Image im1 = new Image { ID = 1, PropertyRef = 101, Position = 1, Path = "Taa7f469-72fb-4f9b-9861-cd5445dd3486.jpeg" };
        private static Image im2 = new Image { ID = 2, PropertyRef = 101, Position = 2, Path = "Ta757997-283f-4e32-95af-71460a81c87f.png" };
        private static Image im3 = new Image { ID = 3, PropertyRef = 101, Position = 3, Path = "Tdb635bf-6d9f-45db-8da9-837f2cea0db8.jpeg" };

        private static Image im4 = new Image { ID = 4, PropertyRef = 102, Position = 1, Path = "Tea369fe-8059-44f6-8b48-b17211d8d26b.jpeg" };
        private static Image im5 = new Image { ID = 5, PropertyRef = 102, Position = 2, Path = "Taa72703-a50a-40c0-9871-a85cb78837c8.jpeg" };

        private static string testingDir = "C:\\Users\\windev\\Documents\\git\\WebDevApp\\HousingTestProj2\\";
        private static string b64Preamble = "data:image/";
        private static string b64Postamble = ";base64,";
        private static string kowalskiImgFileContent = System.IO.File.ReadAllText(testingDir + "kowalski64.txt").Trim();
        private static string graterImgFileContent = System.IO.File.ReadAllText(testingDir + "grater64.txt").Trim();
        private static string kowalskiImgComplete = b64Preamble + "png" + b64Postamble + kowalskiImgFileContent;
        private static string graterImgComplete = b64Preamble + "jpeg" + b64Postamble + graterImgFileContent;
        private static string filestoreRoot = "C:\\temp\\";

        [Fact]
        public async void AddImageRefToDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "AddImageRefToDB")
                .Options;
            ;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                ImageService ims = new ImageService(mapper, context);
                await ims.Add(im1);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                var rows = context.Image.Count();
                Assert.Equal(1, rows);
                Assert.Equal(im1, context.Image.Single());
            }
        }

        [Fact]
        public async void RemoveImageRefFromDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "RemoveImageFromDB")
                .Options;
    

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Image.Add(im1);
                context.Image.Add(im2);
                context.Image.Add(im3);
                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                ImageService ims = new ImageService(mapper, context);
                ims.Remove(im2);
                var rows = context.Image.Count();
                Assert.Equal(2, rows);
                Assert.Equal(new List<Image> { im1, im3 }, context.Image.ToList());
            }
        }

        [Fact]
        public async void GetPropertyImagesFromDB()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "GetImagesFromDB")
                .Options;


            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Image.Add(im1);
                context.Image.Add(im2);
                context.Image.Add(im3);
                context.Image.Add(im4);
                context.Image.Add(im5);

                await context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new HousingContext(options))
            {
                ImageService ims = new ImageService(mapper, context);
                List<Image> images = ims.GetByProperty(101);
                Assert.Equal(new List<Image> { im1, im2, im3 }, images);
            }
        }

        [Fact]
        public void AddAllWritesRefsToDBAndImagesToDisk()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "AddImagesToDBAndDisk")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            string kowalskiShortPath = "";
            string graterShortPath = "";
            string kowalskiLongPath = "";
            string graterLongPath = "";
            List<Image> images = null;

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                ImageService ims = new ImageService(mapper, context);
                ims.AddAll(100, new List<string> { kowalskiImgComplete, graterImgComplete });

                //Expected order, validate at end
                images = context.Image.Where(i => i.PropertyRef == 100).OrderBy(i => i.Position).ToList();
                kowalskiShortPath = images[0].Path;
                graterShortPath = images[1].Path;
                kowalskiLongPath = filestoreRoot + kowalskiShortPath;
                graterLongPath = filestoreRoot + graterShortPath;
            }

            Image kowalskiExpected = new Image { ID = images[0].ID, PropertyRef = 100, Position = 1, Path = kowalskiShortPath };
            Image graterExpected = new Image { ID = images[1].ID, PropertyRef = 100, Position = 2, Path = graterShortPath };
            
            using (var context = new HousingContext(options))
            {

                Image kowalski = images[0];
                Image grater = images[1];

                Assert.NotNull(kowalski);
                Assert.NotNull(grater);

                Assert.Equal(kowalski, kowalskiExpected);
                Assert.Equal(grater, graterExpected);

                //Compare to the image sent in, if ok objects are ordered as expected
                Assert.Equal(kowalskiImgFileContent, Convert.ToBase64String(System.IO.File.ReadAllBytes(kowalskiLongPath)));
                Assert.Equal(graterImgFileContent, Convert.ToBase64String(System.IO.File.ReadAllBytes(graterLongPath)));
            }

            System.IO.File.Delete(kowalskiLongPath);
            System.IO.File.Delete(graterLongPath);
        }
 
        [Fact]
        public async void RemoveAllRemovesRefsFromDBAndImagesFromDisk()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "RemoveImagesFromDBAndDisk")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                context.Image.Add(im1);
                context.Image.Add(im2);
                System.IO.File.WriteAllText(filestoreRoot + im1.Path, kowalskiImgComplete);
                System.IO.File.WriteAllText(filestoreRoot + im2.Path, graterImgComplete);
                await context.SaveChangesAsync();
            }

            // Run the test against one instance of the context
            using (var context = new HousingContext(options))
            {
                ImageService ims = new ImageService(mapper, context);
                ims.DeleteAll(101);
            }

            using (var context = new HousingContext(options))
            {
                Assert.Null(context.Image.Find(1));
                Assert.Null(context.Image.Find(2));
            }

            Assert.False(System.IO.File.Exists(filestoreRoot + im1.Path));
            Assert.False(System.IO.File.Exists(filestoreRoot + im2.Path));
        }

        [Fact]
        public async void UpdateUpdatesRefOnDBAndImageOnDisk()
        {
            var options = new DbContextOptionsBuilder<HousingContext>()
                .UseInMemoryDatabase(databaseName: "UpdateImagesOnDBAndDisk")
                .Options;

            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            // Run the test against one instance of the context
            // Add kowalski
            using (var context = new HousingContext(options))
            {
                context.Image.Add(im1);
                System.IO.File.WriteAllText(filestoreRoot + im1.Path, kowalskiImgComplete);
                await context.SaveChangesAsync();
            }
            
            using (var context = new HousingContext(options))
            {
                ImageService ims = new ImageService(mapper, context);
                ims.Update(101, new List<string> { graterImgComplete });
            }

            Image imNew = null;
            using (var context = new HousingContext(options))
            {
                imNew = context.Image.Find(1);
                Assert.Equal(im1.Position, imNew.Position);
                Assert.Equal(im1.PropertyRef, imNew.PropertyRef);
            }
            
            List<string> fileState = System.IO.Directory.GetFiles(filestoreRoot).ToList();
            Assert.DoesNotContain(filestoreRoot + im1.Path, fileState);
            Assert.Contains(filestoreRoot + imNew.Path, fileState);
            Assert.Equal(graterImgFileContent, Convert.ToBase64String(System.IO.File.ReadAllBytes(filestoreRoot + imNew.Path)));

            System.IO.File.Delete(filestoreRoot + imNew.Path);
        }


    }
}
