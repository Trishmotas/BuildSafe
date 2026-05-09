# BuildSafe

## BIM-Based Safety Analysis Plugin for Autodesk Revit

### An uncertainty-aware BIM safety analysis framework supporting Prevention through Design (PtD)

BuildSafe is a Revit-integrated BIM safety analysis prototype that supports Prevention through Design (PtD) through rule-based and uncertainty-aware hazard identification workflows.

---
    

# Overview

BuildSafe is a BIM-based Revit safety analysis plugin prototype developed to support Prevention through Design (PtD) principles in high-rise construction projects.

The system applies rule-based hazard detection and assumption-based risk classification to identify selected construction safety hazards directly within Autodesk Revit during the design stage.

The plugin was developed as part of an MSc BIM research and consultancy project focused on improving design-stage hazard awareness using BIM-enabled workflows.

BuildSafe is intended as a decision-support system rather than a fully automated safety verification tool.

---



# Key Features

- Revit-integrated safety analysis workflow
- Rule-based hazard identification
- Fall-risk detection
- Long-span structural risk detection
- Assumption-based risk classification
- Confidence-level reporting
- Structured hazard reporting
- Hazard highlighting within Revit
- CSV safety report export
- Integrated safety dashboard

---



# System Workflow

```text
Revit Model
    ↓
Rule Engine
    ↓
Hazard Classification
    ↓
Confidence Logic
    ↓
Reporting + Visualization
```

---


# Assumption-Based Risk Logic

One of the core contributions of BuildSafe is its handling of incomplete BIM safety information.

In many real-world BIM workflows, temporary protection systems such as:

- Guardrails
- Temporary edge protection
- Scaffolding
- Temporary barriers

are often not explicitly represented within BIM models during early design stages.

Rather than assuming safety conditions are satisfied, BuildSafe classifies these situations as assumption-based hazards with lower confidence levels.



Example:

```text
Confidence Level: Low

Hazard Classification: Assumed Risk

Description:
Elevated slab edge with no edge protection modelled

Recommendation:
Verify temporary guardrails or edge protection installation
```

This improves transparency and supports more informed design-stage decision-making.

---



# Screenshots

## BuildSafe Safety Analysis Dashboard

![Dashboard](screenshots/buildsafe-dashboard.png)

---

## Assumption-Based Hazard Classification

![Assumption Risk](screenshots/assumption-based-risk.png)

---

## Revit Integration

![Ribbon](screenshots/revit-ribbon.png)

---



# Technology Stack

| Component | Technology |
| ---------- | ---------- |
| BIM Platform | Autodesk Revit 2026 |
| Programming Language | C# |
| API | Autodesk Revit API |
| IDE | Microsoft Visual Studio |
| Framework | .NET |
| Reporting | CSV Export |
| Architecture | Modular Rule-Based Framework |

---


# Repository Structure

```text
BuildSafe/
│
├── docs/
├── sample-exports/
├── sample-models/
├── screenshots/
├── src/
├── README.md
├── .gitignore
└── .gitattributes
```

---



# Current Implemented Features

| Capability | Status |
| ---------- | ---------- |
| Revit ribbon integration | Implemented |
| Single-click safety analysis | Implemented |
| Hazard highlighting | Implemented |
| Fall-risk detection | Implemented |
| Long-span risk detection | Implemented |
| Confidence scoring | Implemented |
| Assumption-based risk logic | Implemented |
| Structured reporting | Implemented |
| Hazard export workflow | Implemented |
| Revit model interaction | Implemented |

---



# Installation

## Requirements

- Autodesk Revit 2026
- Microsoft Visual Studio
- .NET Framework compatible with Revit 2026

## Setup

1. Clone the repository
2. Open the solution in Visual Studio
3. Build the solution
4. Copy the generated add-in files to the Revit Addins folder
5. Launch Revit
6. Open the BuildSafe tab

---


# Current Limitations

BuildSafe is a research prototype and currently has several limitations:

- Hazard detection depends on BIM model quality and completeness
- Temporary protection systems are often not modelled explicitly
- The system currently focuses on selected high-risk hazard categories
- Static BIM analysis cannot fully represent dynamic construction processes
- The tool is intended to support professional judgement rather than replace safety review processes

---


# Future Development

Potential future development areas include:

- Opening and shaft detection expansion
- Additional hazard categories
- Construction sequence integration
- Enhanced rule-based logic
- Expanded reporting and dashboard systems
- Multi-project deployment workflows

---


# Professional Context

BuildSafe was developed as part of an MSc BIM consultancy and digital engineering project focused on improving design-stage safety awareness through BIM-enabled workflows and rule-based hazard analysis.

The prototype explores how uncertainty-aware BIM workflows can support Prevention through Design (PtD) decision-making during early-stage project development.

Key focus areas include:

- BIM-enabled safety analysis
- Prevention through Design (PtD)
- Rule-based hazard identification
- Construction safety workflows
- Uncertainty-aware BIM analysis
- Design-stage risk management
- Digital engineering workflow integration

---

# Important Disclaimer

BuildSafe is an early-stage prototype developed for professional evaluation, academic investigation, and BIM workflow exploration.

The plugin is intended to support design-stage hazard awareness and decision-making workflows. It does not replace formal safety review procedures, CDM compliance processes, temporary works design, or project-specific risk assessments.

---



# Author

Trish O. Asemota  
MSc BIM in Design, Construction, Operations and Maintenance

*Building resilient futures through BIM, AI, sustainability & smart design.*

---

# License

This project is released for academic and research purposes.
