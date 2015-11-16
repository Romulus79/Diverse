using Fall2015.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fall2015.Repositories;
using Fall2015.ViewModels;

namespace Fall2015.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsRepository studentsRepository;

        private readonly IEmailer _emailer;

        //you should not do this, now that you know D.I.
        CompetencyHeadersRepository competencyHeadersRepository = new CompetencyHeadersRepository();

        public StudentsController(
            IStudentsRepository studentsRepository, 
            IEmailer emailer)
        {
            this.studentsRepository = studentsRepository;
            _emailer = emailer;
        }

        public ActionResult GridExample()
        {
            return View();
        }


        public ActionResult Index()
        {
            StudentIndexViewModel sivm = new StudentIndexViewModel
            {
                Students = studentsRepository.All.ToList(),
                CompetencyHeaders = competencyHeadersRepository.All.ToList()
            };

            return View(sivm);
        }

        [HttpGet]
        public ActionResult Edit(int studentId)
        {
            //look up a student in the db
            Student student = studentsRepository.Find(studentId);
            return View(student);
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            //save to db.
            //if you edit student
            if (ModelState.IsValid)
            {
                studentsRepository.InsertOrUpdate(student);
                studentsRepository.Save();
                return RedirectToAction("Index");
            }


            return View(student);


        }




        [HttpGet]
        public ActionResult Create()
        {
            CreateEditStudentViewModel vm =
                new CreateEditStudentViewModel
                {
                    Student = new Student(),
                    CompetencyHeaders = 
                    competencyHeadersRepository.AllIncluding(
                        a => a.Competencies).ToList()
                };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(Student student, 
            HttpPostedFileBase image, IEnumerable<int> compIds)
        {
            if (ModelState.IsValid)
            {
                studentsRepository.InsertOrUpdate(student);

                string path = Server != null ? Server.MapPath("~") : "";

                student.SaveImage(image, path , "/ProfileImages/");
                studentsRepository.Save();
                _emailer.Send("Welcome to our website...");
                return View("Thanks");
            }
            else
            {
                return View();
            }
        }



        public String WannaPlayDad()
        {
            return "No!";
        }

        public ActionResult WannaPlayDad2()
        {
            ViewBag.Dad = "Hi there sonny";
            return View();
        }
    }
}




