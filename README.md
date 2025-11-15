 # Mars Onboarding Project
 BDD Test Automation Framework using C#, Selenium, NUnit, and Reqnroll

 ## Project Overview
 This project automates the functional testing of the Mars Onboarding Web Application using C#, Selenium WebDriver, and NUnit, with Reqnroll for Behavior-Driven Development (BDD).

The framework is built using the Page Object Model (POM) design pattern to promote modularity, maintainability, and reusability. 
Each test scenario is written in Gherkin syntax, providing clear and human-readable test documentation.

## Tech Stack
Language: C#
Automation Tool: Selenium WebDriver
Test Framework: NUnit
BDD Framework: Reqnroll (SpecFlow)
Design Pattern: Page Object Model (POM)
IDE: Visual Studio 2022
Build Tool: .NET 8

## Implementation Approach
** The framework follows the Page Object Model (POM) pattern — separating test logic from UI locators.
** BDD (Gherkin) syntax is used for writing test scenarios that describe system behavior in a readable format.
** Hooks.cs handles setup and teardown — launching and closing browsers.
** Page classes contain locators and reusable methods for web page interactions.
** Step Definition classes connect feature file steps to the correspondng page actions.

## How to Run the Tests
# Prerequisites
Visual Studio 2022 (or later)
.NET 8 SDK or newer
Chrome browser

# NuGet Packages
Selenium.WebDriver
Selenium.Support
NUnit
NUnit3TestAdapter
Reqnroll




