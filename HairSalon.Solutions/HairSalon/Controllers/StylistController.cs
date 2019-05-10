using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;


namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {

    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }


        [HttpGet("/stylists/new")]
        public ActionResult New()
        {
          return View();
        }

        [HttpPost("/stylists")]
        public ActionResult Create(string stylistName, string stylistSpecialty)
        {
          Stylist newStylist = new Stylist(stylistName, stylistSpecialty);
          List<Stylist> allStylists = Stylist.GetAll();
          newStylist.Save();
          // return View("Index", allStylists);
          return RedirectToAction("Index");
        }
        //modified Controller and Show View to accept a single stylist object
        //instead of a Dictionary (we thought dictionary was redundant)

        [HttpGet("/stylists/{id}")]
        public ActionResult Show(int id)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Stylist selectedStylist = Stylist.Find(id);
          List<Client> stylistClients = selectedStylist.GetClients();
          model.Add("stylist", selectedStylist);
          model.Add("clients", stylistClients);
          // return View(model);
          return View(selectedStylist);
        }
        // This one creates new Clients within a given Stylist, not new Stylists:

        [HttpPost("/stylists/{stylistId}/clients")]
        public ActionResult Create(string nameClient, int stylistId)
        {
          // Dictionary<string, object> model = new Dictionary<string, object>();
          Stylist foundStylist = Stylist.Find(stylistId);
          Client newClient = new Client(nameClient, stylistId);
          newClient.Save();
          foundStylist.GetClients();
          // List<Client> stylistClients = foundStylist.GetClients();
          // model.Add("clients", stylistClients);
          // model.Add("stylist", foundStylist);
          return View("Show", foundStylist);
        }

        [HttpPost("/stylists/{stylistId}/delete-stylist")]
        public ActionResult DeleteStylist(int stylistId)
        {
          Stylist selectedStylist = Stylist.Find(stylistId);
          selectedStylist.DeleteCat(stylistId);
          Dictionary<string, object> model = new Dictionary<string, object>();
          List<Client> stylistClients = selectedStylist.GetClients();
          model.Add("stylist", selectedStylist);
          return RedirectToAction("Index", "Stylists");

          //
          // Client item = Client.Find(itemId);
          // item.Delete();
          // Dictionary<string, object> model = new Dictionary<string, object>();
          // Stylist foundStylist = Stylist.Find(stylistId);
          // List<Client> stylistClients = foundStylist.GetClients();
          // model.Add("item", stylistClients);
          // model.Add("stylist", foundStylist);
          // // return View(model);
          // return RedirectToAction("Show", "Stylists");
          // //return RedirectToAction("actionName", "controllerName"); goes to a cshtml page in a different controller.
        }





  }
}
