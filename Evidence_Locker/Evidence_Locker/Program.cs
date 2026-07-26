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

string dataFolder = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(dataFolder);

ICaseRepository caseRepository = new CaseRepository(Path.Combine(dataFolder, "cases.json"));
IEvidenceRepository evidenceRepository = new EvidenceRepository(Path.Combine(dataFolder, "evidence.json"));

ICaseService caseService = new CaseService(caseRepository);
IEvidenceService evidenceService = new EvidenceService(evidenceRepository, caseRepository);
IReportService reportService = new ReportService(caseRepository);

var caseMenu = new CaseMenu(caseService);
var evidenceMenu = new EvidenceMenu(evidenceService);
var reportMenu = new ReportMenu(reportService);

var menuRenderer = new MenuRenderer(caseMenu, evidenceMenu, reportMenu);
menuRenderer.Run();

