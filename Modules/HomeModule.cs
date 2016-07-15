using System.Collections.Generic;
using System;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Stylist> stylists = Stylist.GetAll();
        return View["index.cshtml", stylists];
      };
      Post["/stylists/new"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["name"], Request.Form["avail"], Request.Form["notes"]);
        newStylist.Save();
        List<Stylist> stylists = Stylist.GetAll();
        return View["index.cshtml", stylists];
      };
      Get["/stylists/{id}"] = parameter => {
        Stylist foundStylist = Stylist.Find(parameter.id);
        return View["stylist.cshtml", foundStylist];
      };
      Post["/clients/new"] = _ => {
        string newName = (string)Request.Form["name"];
        int stylistId = (int)Request.Form["stylist_id"];
        string newNotes = (string)Request.Form["notes"];
        Client newClient = new Client(newName, stylistId, newNotes);
        newClient.Save();
        Stylist foundStylist = Stylist.Find(stylistId);
        return View["stylist.cshtml", foundStylist];
      };
      Get["/clients/{id}"] = parameter => {
        Client foundClient = Client.Find(parameter.id);
        List<object> model = new List<object> {};
        model.Add(foundClient);
        model.Add(Stylist.GetAll());
        return View["client.cshtml", model];
      };
      Patch["/clients/edit/{id}"] = parameter => {
        Client editedClient = Client.Find(parameter.id);
        List<string> updateColumns = new List<string> {"name", "stylist_id", "notes"};
        int newStylistId = (int)Request.Form["stylist_id"];
        string newName = (string)Request.Form["name"];
        string newNotes = (string)Request.Form["notes"];
        List<object> updateValues = new List<object> {newName, newStylistId, newNotes};
        editedClient.Update(updateColumns, updateValues);
        List<object> model = new List<object> {};
        model.Add(editedClient);
        model.Add(Stylist.GetAll());
        return View["client.cshtml", model];
      };
    }
  }
}
