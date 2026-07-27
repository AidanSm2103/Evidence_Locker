using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Core.Interfaces;
using Evidence_Locker.Data.Repositories;
using Evidence_Locker.Services;
using Evidence_Locker.UI;
using Evidence_Locker.UI.Screens;

// --- Composition Root ---
// This is the ONLY file in the whole solution that ever calls `new` on a concrete class
// Every otherfile depends on interfaces and receives its dependencies through its constructor
// This file's entire job is deciding which concrete implementation gets plugged into each interface, in one place

string dataFolder = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(dataFolder);

// Data layer — concrete JSON-backed repositories, held as their interface types so everything built on top of them only ever sees the abstraction
ICaseRepository caseRepository = new CaseRepository(Path.Combine(dataFolder, "cases.json"));
IEvidenceRepository evidenceRepository = new EvidenceRepository(Path.Combine(dataFolder, "evidence.json"));

// Services layer — business logic, each service given exactly the repositories it needs via constructor injection
ICaseService caseService = new CaseService(caseRepository);
IEvidenceService evidenceService = new EvidenceService(evidenceRepository, caseRepository);
IReportService reportService = new ReportService(caseRepository);

// UI layer — each screen given only the service interface(s) it needs, never a repository directly
var caseMenu = new CaseMenu(caseService);
var evidenceMenu = new EvidenceMenu(evidenceService);
var reportMenu = new ReportMenu(reportService);

// Top-level menu loop owns the three screens and routes between them
var menuRenderer = new MenuRenderer(caseMenu, evidenceMenu, reportMenu);
menuRenderer.Run();

