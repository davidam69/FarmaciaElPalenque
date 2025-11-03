// Global Usings para Farmacia El Palenque

// System namespaces
global using System;
global using System.Collections.Generic;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Data;
global using System.Diagnostics;
global using System.Linq;
global using System.Net;
global using System.Net.Mail;
global using System.Threading.Tasks;
global using System.Web;

// ASP.NET Core
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using Microsoft.AspNetCore.Mvc.ViewFeatures;

// Entity Framework
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Migrations;

// ML.NET
global using Microsoft.ML;
global using Microsoft.ML.Data;

// SQL Client
global using Microsoft.Data.SqlClient;

// Email
global using MimeKit;

// PDF
global using QuestPDF.Fluent;
global using QuestPDF.Helpers;
global using QuestPDF.Infrastructure;

// Proyecto específico
global using FarmaciaElPalenque.MlModel;
global using FarmaciaElPalenque.Models;
global using FarmaciaElPalenque.Models.ViewModels;
global using FarmaciaElPalenque.Services;
