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
    }
  }
}
