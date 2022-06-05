using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartER_Garden_Library.SmartER_Garden_Models;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace SmartER_Garden_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartER_Garden_Controller : ControllerBase
    {
        SmartERGardenContext context = new SmartERGardenContext(); 
        //static List<Eintrag> einträge = new List<Eintrag>();


        [HttpGet("einträge")]
        public ActionResult<List<Eintrag>> GetEinträge()
        {
            return Ok(context.Eintrag);
        }

        [HttpGet("pflanzen")]
        public ActionResult<List<Pflanze>> GetPflanzen()
        {
            return Ok(context.Pflanze);
        }

        [HttpGet("züchter")]
        public ActionResult<List<Züchter>> GetZüchter()
        {
            return Ok(context.Züchter);
        }

        [HttpGet("standort")]
        public ActionResult<List<Standort>> GetStandort()
        {
            return Ok(context.Standort);
        }

        [HttpGet("standort")]
        public ActionResult<List<Standort>> GetEssbarePflanzen()
        {
            return Ok(context.EssbarePflanze);
        }

        [HttpGet("standort")]
        public ActionResult<List<Standort>> GetNichtEssen()
        {
            return Ok(context.NichtEssbarePflanzen);
        }

        [HttpPatch("richtigerpfad")]
        public ActionResult PatchEintrag(string eintragsname, int höhe, string lichtzyklus, string düngerschema, int wasserzufuhr, int wochenzahl, DateTime datum)
        {
            var eintrag = context.Eintrag.Where(a => a.EintragsName = eintragsname).FirstorDefault();
            if (eintrag == null)
            {
                NotFound();
                return null; 
            }
            else
            {
                eintrag.Eintragsname = eintragsname;
                eintrag.Höhe = höhe;
                eintrag.Lichtzyklus = lichtzyklus;
                eintrag.Düngerschema = düngerschema;
                eintrag.Wasserzufuhr = wasserzufuhr;
                eintrag.Wochenzahl = wochenzahl;
                eintrag.Datum = datum;

                return eintrag;
            }
        }

        [HttpPost("richtigerpfad")]
        public ActionResult<Eintrag> PostEintrag(string eintragsname, int höhe, string lichtzyklus, string düngerschema, int wasserzufuhr, int wochenzahl, DateTime datum)
        {
            var id = context.Eintrag.Max(a => a.Id) + 1;
            var eintrag = new Eintrag(id, eintragsname, höhe, lichtzyklus, düngerschema, wasserzufuhr, wochenzahl, datum);
            return Ok(eintrag); 

        }

        [HttpPost("richtigerpfad")]
        public ActionResult<Pflanze> PostPflanze(string name, string oberunterirdisch, DateTime pflanzbeginn, DateTime pflanzende)
        {
            var id = context.Pflanze.Max(a => a.Id) + 1;
            var pflanze = new Pflanze(id, name, oberunterirdisch, pflanzbeginn, pflanzende);
            return Ok(pflanze);

        }

        [HttpPost("richtigerpfad")]
        public ActionResult<Züchter> PostEintrag(string name, string beschreibung, string anschrift)
        {
            var id = context.Eintrag.Max(a => a.Id) + 1;
            var züchter = new Züchter(id, name, beschreibung, anschrift);
            return Ok(züchter);

        }

        [HttpPost("richtigerpfad")]
        public ActionResult<Standort> PostStandort(string name, string beschreibung)
        {
            var id = context.Eintrag.Max(a => a.Id) + 1;
            var standort = new Standort(id, name, beschreibung);
            return Ok(standort);
        }

        [HttpDelete("richtigerpfad")]
        public ActionResult DeleteEintrag(string eintragsname)
        {
            var eintrag = context.Eintrag.Where(a => a.EintragsName = eintragsname).FirstorDefault();
            if (eintrag == null)
            {
                NotFound();
                return null; 
            }
            else
            {
                context.Eintrag.Remove(eintrag);
                return Ok(); 
            }
        }

    }
}
