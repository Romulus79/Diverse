﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Fall2015.Helpers;

namespace Fall2015.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Wrong, stupid user. You must have a firstname.")]
        public String Firstname { get; set; }

        [Required]
        public String Lastname { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        public String MobilePhone { get; set; }

        public String ProfileImagePath { get; set; }

        //part of the many-to-many relationsship between Student and Competency
        public ICollection<Competency> Competencies { get; set; }


        public void SaveImage(HttpPostedFileBase image, 
            String serverPath, String pathToFile)
        {
            if (image == null) return;

            string filename = Guid.NewGuid().ToString();
            ImageModel.ResizeAndSave(
                serverPath + pathToFile, filename,
                image.InputStream, 200 );

            ProfileImagePath = pathToFile + filename +
            ".jpg";
        }
    }
}