using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApp.DbHandler.Models;
using WebApp.Models.Admin;

namespace WebApp.DbHandler.Interfaces {

	public interface ICourseHandler {

		public List<Course> LoadAll();

		public List<Module> GetAllModules();

		public List<Module> GetAllModulesById(int courseId);

		public bool AddNewCourse(Course model);

		public string GetCourseNameById(int courseId);

		public string GetModuleNameById(int moduleId);

		public Course GetCourseById(int courseId);

		public bool Update(Course model);
	}

	public class CourseHandler : ICourseHandler {
		private readonly DataContext db;

		public CourseHandler(DataContext _db) {
			this.db = _db;
		}

		public List<Course> LoadAll() {
			var Data = db.Courses;
			List<Course> allCourses = Data.ToList();
			return allCourses;
		}

		public List<Module> GetAllModules() {
			var Data = db.Modules;
			List<Module> allModules = Data.ToList();
			return allModules;
		}

		public List<Module> GetAllModulesById(int courseId) {
			var Data = db.Modules.Where(x => x.CourseId == courseId);
			List<Module> allModules = Data.ToList();
			return allModules;
		}

		public bool AddNewCourse(Course model) {
			//Course newCourse = new Course() {
			//	Name = model.Name,
			//	Description = model.Description,
			//	Icon = model.Icon
			//};

			//Den koden er vel unødvendig? ^

			db.Courses.Add(model);
			db.SaveChanges();

			/* Burde antakeligvis være noe feilsjekk her */
			return true;
		}

		public string GetCourseNameById(int courseId) {
			var Data = db.Courses.Where(x => x.Id == courseId).FirstOrDefault();
			if (Data != null)
				return Data.Name;
			return null;
		}

		public string GetModuleNameById(int moduleId) {
			var Data = db.Modules.Where(x => x.Id == moduleId).FirstOrDefault();
			if (Data != null)
				return Data.Name;
			return null;
		}

		public Course GetCourseById(int courseId) {
			var course = db.Courses.Where(x => x.Id == courseId);
			if (!course.Any()) {
				Console.WriteLine($"ERROR: Could not find course with id: {courseId}");
				return new Course();
			}
			return course.First();
		}

		public bool Update(Course model) {
			db.Courses.Update(model);
			var result = db.SaveChanges();
			//Returns true if more than 0 rows affected
			return result > 0;
		}
	}
}