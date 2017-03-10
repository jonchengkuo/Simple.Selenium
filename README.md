# Simple.Selenium

Simple Selenium is a C# class library that is built on top of the Selenium WebDriver framework to provide user-oriented interactions with UI controls on web pages. It significantly reduces the steep learning curve and necessary foundation work in using Selenium for web UI automation.

## Features

* It provides easy-to-use UI control classes (e.g., Button, TextField, Table, etc.) to interact with the UI controls on web pages.
* It hides many Selenium implementation details such as smart, fluent wait, access of HTML attributes, etc..
* It supports Ajax and modern web applications with smart implicit wait.
* It simplifies the creation of page objects.
* With its default WebDriver usage, you can avoid passing a WebDriver instance all over your code.
* It shows clear error messages when something goes wrong (e.g., it shows the UI control type and locator in error messages).

## Design Principle

Within acceptable performance, it tries to make the coding with Selenium as simple as possible.

## Dependencies

* Selenium.WebDriver
* Selenium.Support

## Installation

TBD.

## Credits
* This project is inspired by the [NSpectator.Selenium](https://github.com/nspectator/NSpectator.Selenium) project.
