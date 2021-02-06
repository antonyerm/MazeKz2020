﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMaze.Models.Certificates;
using WebMaze.Filters;
using WebMaze.Services;

namespace WebMaze.Controllers
{
    public class CertificatesController : Controller
    {
        private readonly CertificateService certificateService;

        public CertificatesController(CertificateService certificateService)
        {
            this.certificateService = certificateService;
        }

        [ImportModelStateErrorsFromTempData]
        public async Task<IActionResult> Index(string userLogin, string certificateName)
        {
            List<CertificateViewModel> certificates;

            if (!string.IsNullOrWhiteSpace(certificateName))
            {
                certificates = await certificateService.GetCertificatesByName(certificateName);
            }
            else if (!string.IsNullOrWhiteSpace(userLogin))
            {
                certificates = await certificateService.GetUserCertificates(userLogin);
            }
            else
            {
                certificates = await certificateService.GetCertificatesAsync();
            }

            return View(certificates);
        }

        public ViewResult Get()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Get(long id)
        {
            var certificateViewModel = await certificateService.GetCertificateAsync(id);

            return View(certificateViewModel);
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CertificateViewModel certificate)
        {
            if (ModelState.IsValid)
            {
                var operationResult = await certificateService.CreateCertificateAsync(certificate);

                if (!operationResult.Succeeded)
                {
                    operationResult.Errors.ForEach(error => ModelState.AddModelError(string.Empty, error));
                }
            }
            
            return View(certificate);
        }

        [HttpPost]
        [ExportModelStateErrorsToTempData]
        public async Task<IActionResult> Issue(string userLogin)
        {
            var operationResult = await certificateService.IssueCertificate("Birth Certificate", userLogin,
                "Government", "The certificate documents the birth of the person", TimeSpan.FromDays(3650));
            
            if (!operationResult.Succeeded)
            {
                operationResult.Errors.ForEach(error => ModelState.AddModelError(string.Empty, error));
            }

            var urlReferrer = Request.Headers["Referer"].ToString();

            return Redirect(urlReferrer);

            /* Second approach, a lot of code duplicates:

            var uri = new Uri(urlReferrer);
            var queryDictionary = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
            
            var certificates = new List<CertificateViewModel>();

            if (!queryDictionary.Any())
            {
                certificates = await certificateService.GetCertificatesAsync();
            }
            else if(!string.IsNullOrWhiteSpace(queryDictionary["certificateName"]))
            {
                certificates = await certificateService.GetCertificatesByName(queryDictionary["certificateName"]);
            }
            else if (!string.IsNullOrWhiteSpace(queryDictionary["userLogin"]))
            {
                certificates = await certificateService.GetUserCertificates(queryDictionary["userLogin"]);
            }

            return View(nameof(Index), certificates);
            */
        }

        [HttpPost]
        [ExportModelStateErrorsToTempData]
        public async Task<IActionResult> Revoke(string certificateName, string userLogin)
        {
            var operationResult = await certificateService.RevokeCertificate(certificateName, userLogin);

            if (!operationResult.Succeeded)
            {
                operationResult.Errors.ForEach(error => ModelState.AddModelError(string.Empty, error));
            }

            var urlReferrer = Request.Headers["Referer"].ToString();

            return Redirect(urlReferrer);
        }

        public async Task<IActionResult> Update(long id)
        {
            var certificateViewModel = await certificateService.GetCertificateAsync(id);

            return View(certificateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CertificateViewModel certificateViewModel)
        {
            if (ModelState.IsValid)
            {
                var operationResult = await certificateService.UpdateCertificateAsync(certificateViewModel);

                if (!operationResult.Succeeded)
                {
                    operationResult.Errors.ForEach(error => ModelState.AddModelError(string.Empty, error));
                }
            }

            return View(certificateViewModel);
        }

        [HttpPost]
        [ExportModelStateErrorsToTempData]
        public async Task<IActionResult> Delete(long id)
        {
            var operationResult = await certificateService.DeleteCertificateAsync(id);

            if (!operationResult.Succeeded)
            {
                operationResult.Errors.ForEach(error => ModelState.AddModelError(string.Empty, error));
            }

            var urlReferrer = Request.Headers["Referer"].ToString();

            return Redirect(urlReferrer);
        }
    }
}
