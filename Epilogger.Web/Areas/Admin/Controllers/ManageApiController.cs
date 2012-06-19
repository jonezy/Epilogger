using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Models;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

namespace Epilogger.Web.Areas.Admin.Controllers
{

    public partial class ManageApiController : Controller
    {
        EpiloggerDB _db = new EpiloggerDB();
        APIApplicationService _as = new APIApplicationService();

        private struct CipherPrivateKey
        {
            public byte[] modulus;
            public byte[] publicExponent;
            public byte[] privateExponent;
            public byte[] p;
            public byte[] q;
            public byte[] dP;
            public byte[] dQ;
            public byte[] qInv;
        }
        private struct CipherPublicKey
        {
            public bool isPrivate;
            public byte[] modulus;
            public byte[] exponent;
        }


        //
        // GET: /Admin/ManageAPI/

        public virtual ActionResult Index()
        {
            var theUsers = _db.Users.Where(e => e.RoleID == 3);
            return View(theUsers);
        }

        //
        // GET: /Admin/ManageAPI/Edit/5

        public virtual ActionResult Edit(Guid userId)
        {
            var theApiApplication = _as.FindByUserId(userId);

            //If the clientID and secret are blank, generate them.
            if (String.IsNullOrEmpty(theApiApplication.ClientID))
            {
                //Generate Keys
                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    try
                    {
                        RSAParameters rsaParams = rsa.ExportParameters(true);

                        theApiApplication.ClientID = GetHexString(rsaParams.Modulus);
                        theApiApplication.ClientSecret = GetHexString(rsaParams.D);
                    }
                    finally
                    {
                        rsa.PersistKeyInCsp = false;
                    }
                }

            }

            return View(theApiApplication);
        }




        private string GetHexString(byte[] byteArray)
        {
            StringBuilder hexString = new StringBuilder(byteArray.Length * 2);
            for (int i = 0; i < byteArray.Length; i++)
                hexString.Append(string.Format("{0:X}", byteArray[i]));
            int x = hexString.Capacity;
            return hexString.ToString();
        }



        //
        // POST: /Admin/ManageAPI/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(APIApplication model)
        {
            try
            {
                
                _as.Save(model);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
