using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Caalinder.Data;
using Caalinder.Models;
using Caalinder.Observer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Caalinder.Controllers
{

    public class MatchController : Controller
    {
        static  MementoDeslike mementoLike = new MementoDeslike(null);
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<string> errors = new List<string>();
        // GET: Match
        public ActionResult Index()
        {

            IEnumerable<HorseModel> horses = new List<HorseModel>();
            string userid = User.Identity.GetUserId();
            horses = db.HorseModels.Where(h => h.ApplicationUserId != userid);
            return View(horses.ToList());
        }


        // GET: Match/Details/5
        public ActionResult SelecionarCavalo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HorseModel horseModel = db.HorseModels.Find(id);
            if (horseModel == null)
            {
                return HttpNotFound();
            }
            IEnumerable<HorseModel> horses = new List<HorseModel>();
            string userid = User.Identity.GetUserId();
            horses = db.HorseModels.Where(h => h.ApplicationUserId == userid && h.Gender != horseModel.Gender);
            ViewBag.Name = new SelectList(horses, "Id", "Name", horseModel.Name);
            HorseViewModel horseViewModel = AutoMapper.Mapper.Map<HorseModel, HorseViewModel>(horseModel);
            return View(horseViewModel);
        }
        public ActionResult Confirmar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmar(FormCollection form)
        {
            
            IEnumerable<MatchModel> matchlist = new List<MatchModel>();
            MatchModel matchfeito = new MatchModel();
            bool likou = false;

            int IdMeuCavaloEscolhido = Convert.ToInt32(form["Name"].ToString());
            int IdCavaloEscolhido = Convert.ToInt32(form["IdCavalo"].ToString());
            HorseModel horsecavaloescolhido = db.HorseModels.SingleOrDefault(p => p.Id == IdCavaloEscolhido);
            matchlist = db.MatchModels.Where(m => m.Horse1Id == IdCavaloEscolhido);
            foreach (MatchModel model in matchlist)
            {
                if (IdMeuCavaloEscolhido == model.Horse2Id)
                {
                    model.Like1 = true;
                    model.Like2 = true;
                    model.Match = true;
                    model.ApplicationUser1 = horsecavaloescolhido.ApplicationUserId;
                    model.ApplicationUser2 = User.Identity.GetUserId();
                    matchfeito = model;
                    likou = true;
                }
            }
            HorseModel Myhorse = db.HorseModels.Find(IdMeuCavaloEscolhido);
            HorseModel Otherhorse = db.HorseModels.Find(IdCavaloEscolhido);
            if (Myhorse.Gender != Otherhorse.Gender)
            {
                if (likou) updateMatch(matchfeito);
                else AddNewMatch(true, false, User.Identity.GetUserId(),horsecavaloescolhido.ApplicationUserId , IdMeuCavaloEscolhido, IdCavaloEscolhido);
                return RedirectToAction("Index");
            }
            else
            {
                errors.Add("Cavalo de mesmo sexo selecionado");
                AddModelStateError(errors);
                return RedirectToAction("SelecionarCavalo/" + Otherhorse.Id);
            }
        }
        public void updateMatch(MatchModel match)
        {
            db.Entry(match).State = EntityState.Modified;
            db.SaveChanges();
            int likes = 0;
            ObserverModel observer = new ObserverModel("email");    
            List<MatchModel> matchlist = db.MatchModels.Where(p => p.Horse1Id == match.Horse1Id).ToList();
            List<MatchModel> matchlist2 = db.MatchModels.Where(p => p.Horse2Id == match.Horse1Id).ToList();
            foreach (MatchModel matches in matchlist)
            {
                if (matches.Like2)
                {
                    likes++;
                }
            }
            foreach (MatchModel matches in matchlist2)
            {
                if (matches.Like1)
                {
                    likes++;
                }
            }
            observer.Update(likes);
        }

        public void AddNewMatch(bool like1, bool like2, string CurrentUserID,string usercavaloescolhidoID, int horseId1, int horseId2)
        {
            MatchModel Match = new MatchModel();
            Match.Like1 = like1;
            Match.Like2 = like2;
            int likes = 0;
            Match.ApplicationUser1 = CurrentUserID;
            Match.ApplicationUser2 = usercavaloescolhidoID;
            Match.Horse1Id = horseId1;
            Match.Horse2Id = horseId2;
            IEnumerable<MatchModel> matchexist = db.MatchModels.Where(m => (m.Like1 == Match.Like1 &&
            m.Like2 == Match.Like2 && m.Horse1Id == Match.Horse1Id && m.Horse2Id == Match.Horse2Id));

            if (!matchexist.Any())
            {
                ObserverModel observer = new ObserverModel("email");
                db.MatchModels.Add(Match);
                db.SaveChanges();
                List<MatchModel> matchlist = db.MatchModels.Where(p => p.Horse1Id == horseId2).ToList();
                List<MatchModel> matchlist2 = db.MatchModels.Where(p => p.Horse2Id == horseId2).ToList();
                foreach (MatchModel match in matchlist)
                {
                    if (match.Like2)
                    {
                        likes++;
                    }
                }
                foreach (MatchModel match in matchlist2)
                {
                    if (match.Like1)
                    {
                        likes++;
                    }
                }
                observer.Update(likes);
            }
            else
            {
                //exibir mensagem ao usuario dizendo que o mesmo ja deu like entre esses cavalos
            }
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchViewDetails matchViewDetails = new MatchViewDetails();
            MatchModel matchModel = db.MatchModels.Find(id);
            if (User.Identity.GetUserId() == matchModel.ApplicationUser1)
            {
                matchViewDetails.Email = db.Users.Find(matchModel.ApplicationUser2).Email;
                matchViewDetails.Cidade = db.Users.Find(matchModel.ApplicationUser2).Cidade;
                matchViewDetails.Estado = db.Users.Find(matchModel.ApplicationUser2).Estado;
                matchViewDetails.Cavalo = db.HorseModels.Find(matchModel.Horse2Id);
            }
            else
            {
                matchViewDetails.Email = db.Users.Find(matchModel.ApplicationUser1).Email;
                matchViewDetails.Cidade = db.Users.Find(matchModel.ApplicationUser1).Cidade;
                matchViewDetails.Estado = db.Users.Find(matchModel.ApplicationUser1).Estado;
                matchViewDetails.Cavalo = db.HorseModels.Find(matchModel.Horse1Id);
            }
            if (matchModel == null)
            {
                return HttpNotFound();
            }
            return View(matchViewDetails);
        }

        public ActionResult Create()
        {
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Horse1Id,Horse2Id,Like1,Like2,Match")] MatchModel matchModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.MatchModels.Add(matchModel);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(matchModel);
        //}

        // GET: Match/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MatchModel matchModel = db.MatchModels.Find(id);
        //    if (matchModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(matchModel);
        //}

        // POST: Match/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Horse1Id,Horse2Id,Like1,Like2,Match")] MatchModel matchModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(matchModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(matchModel);
        //}

        //// GET: Match/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MatchModel matchModel = db.MatchModels.Find(id);
        //    if (matchModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(matchModel);
        //}

        // POST: Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MatchModel matchModel = db.MatchModels.Find(id);
            db.MatchModels.Remove(matchModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult MeusMatches()
        {
            //isso ja ta funcionando passando uma modelview para a view onde tem duas listas uma com os cavalos do usuario e o cavalo
            //com o qual ele deu match sendo respectivos o meu cavalo 1 deu match com o cavalo 1 da outra lista e assim vai
            // não sei mexer em html para aparecer numa tabela bonita mas essa parte ta pronta

            string userID = User.Identity.GetUserId();
            IEnumerable<MatchModel> Matches = db.MatchModels.Where(m => m.ApplicationUser1 == userID
            || m.ApplicationUser2 == userID);
            List<int> MatchIds = new List<int>();
            List<int> MyhorseID = new List<int>();
            List<int> TheirhorseID = new List<int>();
            foreach (MatchModel mates in Matches)
            {

                if (mates.Like1 && mates.Like2 && mates.Match)
                {                   
                    MatchIds.Add(mates.Id);
                    if (mates.ApplicationUser1 == userID)
                    {
                        MyhorseID.Add(mates.Horse1Id);
                    }
                    else TheirhorseID.Add(mates.Horse1Id);

                    if (mates.ApplicationUser2 == userID)
                    {
                        MyhorseID.Add(mates.Horse2Id);
                    }
                    else TheirhorseID.Add(mates.Horse2Id);
                }
            }

            HorseModel temp;
            List<HorseModel> Meuscavalos = new List<HorseModel>();
            List<HorseModel> CavalosDele = new List<HorseModel>();
            foreach (int id in MyhorseID)
            {
                temp = db.HorseModels.Find(id);
                Meuscavalos.Add(temp);

            }
            foreach (int id in TheirhorseID)
            {
                temp = db.HorseModels.Find(id);
                CavalosDele.Add(temp);

            }
            int i = 0;
            MeusMatchesVIewModel matchmodel = new MeusMatchesVIewModel();
            MeusMatchesVIewIndex IndexMatch = new MeusMatchesVIewIndex();
            if (MatchIds.Count > 0)
            {
                while (i < MatchIds.Count)
                {    
                    matchmodel = new MeusMatchesVIewModel();
                    matchmodel.Relike = false;
                    matchmodel.MatchId = MatchIds[i];
                    matchmodel.MeusCavalos = Meuscavalos[i];
                    matchmodel.CavalosDeles = CavalosDele[i];
                    IndexMatch.MeusMatchesList.Add(matchmodel);
                    i++;
                }

            }
            if (mementoLike.getState().Id != 0)
            {
                HorseModel horseModel = new HorseModel();
                matchmodel = new MeusMatchesVIewModel();
                matchmodel.Relike = true;
                int id1 = mementoLike.getState().Horse1Id;
                int id2 = mementoLike.getState().Horse2Id;
             
                if (mementoLike.getState().ApplicationUser1 == User.Identity.GetUserId())
                {
                    matchmodel.MeusCavalos = db.HorseModels.Where(m => m.Id == id1).Single();
                    matchmodel.CavalosDeles = db.HorseModels.Where(m => m.Id == id2).Single();
                }
                else
                {
                    matchmodel.MeusCavalos = db.HorseModels.Where(m => m.Id == id2).Single();
                    matchmodel.CavalosDeles = db.HorseModels.Where(m => m.Id == id1).Single();
                }
                matchmodel.MatchId = mementoLike.getState().Id;
                var item = IndexMatch.MeusMatchesList.SingleOrDefault(x => x.Relike == true);
                IndexMatch.MeusMatchesList.Remove(item);
                IndexMatch.MeusMatchesList.Add(matchmodel);
               
            }
            else
            {
                var item = IndexMatch.MeusMatchesList.SingleOrDefault(x => x.Relike == true);
                IndexMatch.MeusMatchesList.Remove(item);
            }
            return View(IndexMatch);
        }
        public ActionResult Deslike(int? id)
        {
            MatchModel match = new MatchModel();
            match =  db.MatchModels.Where(m => m.Id == id).Single();
            mementoLike = new MementoDeslike(match);

            if (match.ApplicationUser1 == User.Identity.GetUserId())
            {
                match.Like1 = false;
                match.Match = false;
            }
            else if (match.ApplicationUser2 == User.Identity.GetUserId())
            {
                match.Like2 = false;
                match.Match = false;
            }
           
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("MeusMatches");
            }
            return RedirectToAction("MeusMatches");
        }

        public ActionResult Relike(int? id)
        {
            
            MatchModel match = mementoLike.getState();

            if (ModelState.IsValid)
            {

                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                mementoLike = new MementoDeslike(null);
 
                return RedirectToAction("MeusMatches");
            }
            mementoLike = new MementoDeslike(null);

            return RedirectToAction("MeusMatches");
        }
        private void AddModelStateError(List<string> errors)
        {
            foreach (string error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

    }
}
