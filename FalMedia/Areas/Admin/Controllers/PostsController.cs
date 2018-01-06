using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FalMedia.Areas.Admin.ViewModel;
using FalMedia.Controllers;
using FalMedia.Core;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostsController : BaseController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // GET: Admin/Posts
        public ActionResult Index()
        {
            return View(_db.Posts.ToList());
        }

        public ActionResult NewPost()
        {
            var post = new Post {Categories = new List<Category>()};
            PopulateAssignedCategoryData(post);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost(HttpPostedFileBase thumbnail,
            [Bind(
                 Include =
                     "Id,Title,TitleAr,ShortDescription,ShortDescriptionAr,Content,ContentAr,Thumbnail,Status,CreatedBy"
             )] Post post, string[] selectedCategories)
        {
            if (selectedCategories != null)
            {
                post.Categories = new List<Category>();
                foreach (var category in selectedCategories)
                {
                    var categoryToAdd = _db.Categories.Find(int.Parse(category));
                    post.Categories.Add(categoryToAdd);
                }
            }

            string[] allowedExtensions = {".jpg", ".png", ".JPG", ".PNG"};

            if (ModelState.IsValid)
            {
                if (thumbnail != null)
                {
                    var extension = Path.GetExtension(thumbnail.FileName);
                    if (!allowedExtensions.Contains(extension))
                        ModelState.AddModelError("Error", "files extensions not allowed!");
                    var filename = Path.GetFileName(thumbnail.FileName);
                    var renameFile = "post-" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + filename;
                    var path = Path.Combine(Server.MapPath("~/uploads/posts/"), renameFile);
                    thumbnail.SaveAs(path);
                    post.Thumbnail = renameFile;
                }

                post.CreatedBy = User.Identity.Name;
                _db.Posts.Add(post);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateAssignedCategoryData(post);
            return View(post);
        }

        public ActionResult EditPost(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = _db.Posts.Include(c => c.Categories).Single(p => p.Id == id);
            if (post == null)
                return HttpNotFound();

            PopulateAssignedCategoryData(post);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(HttpPostedFileBase thumbnail, [Bind(
                                                                        Include =
                                                                            "Id,Title,TitleAr,ShortDescription,ShortDescriptionAr,Content,ContentAr,Thumbnail,Status,CreatedBy"
                                                                    )] Post post, string[] selectedCategories)
        {
            string[] allowedExtensions = {".jpg", ".png", ".JPG", ".PNG"};

            if (ModelState.IsValid)
            {
                if (thumbnail != null)
                {
                    var extension = Path.GetExtension(thumbnail.FileName);
                    if (!allowedExtensions.Contains(extension))
                        ModelState.AddModelError("Error", "files extensions not allowed!");
                    var filename = Path.GetFileName(thumbnail.FileName);
                    var renameFile = "post-" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + filename;
                    var path = Path.Combine(Server.MapPath("~/uploads/posts/"), renameFile);
                    thumbnail.SaveAs(path);
                    post.Thumbnail = renameFile;
                }

                post.UpdatedDate = DateTime.Now;
                post.CreatedBy = User.Identity.Name;
                post.ModifiedBy = User.Identity.Name;
                _db.Entry(post).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateAssignedCategoryData(post);
            return View(post);
        }


        private void PopulateAssignedCategoryData(Post post)
        {
            var allCategory = _db.Categories;
            var postCategory = new HashSet<int>(post.Categories.Select(c => c.Id));
            var viewModel = allCategory.Select(category => new AssignedCategoryData
            {
                Id = category.Id,
                Name = category.Name,
                Assigned = postCategory.Contains(category.Id)
            }).ToList();
            ViewBag.Category = viewModel;
        }

        private void UpdatePostCategory(string[] selectedCategories, Post postToUpdate)
        {
            if (selectedCategories != null)
            {
                var selectedCategoriesHs = new HashSet<string>(selectedCategories);
                var postCategory = new HashSet<int>(postToUpdate.Categories.Select(c => c.Id));
                foreach (var category in _db.Categories)
                    if (selectedCategoriesHs.Contains(category.Id.ToString()))
                    {
                        if (!postCategory.Contains(category.Id))
                            postToUpdate.Categories.Add(category);
                    }
                    else
                    {
                        if (postCategory.Contains(category.Id))
                            postToUpdate.Categories.Remove(category);
                    }
            }
            postToUpdate.Categories = new List<Category>();
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var post = _db.Posts.Find(id);
            if (post == null)
                return HttpNotFound();
            _db.Posts.Remove(post);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}