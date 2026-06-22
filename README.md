# CyberSafe

CyberSafe is a WPF-based cybersecurity chatbot built in C# that gives advice and tips on a range of cybersecurity topics such as passwords, phishing, scams, privacy, and malware.

## Features

* Keyword recognition for cybersecurity topics
* Randomised responses to keep interactions varied
* Sentiment detection to provide empathetic responses
* Memory system to remember your name and favourite topics
* Follow-up question support ("tell me more", "give me another tip")

## Requirements

* Visual Studio 2022 or later
* .NET 10 SDK

## Database Setup

This project requires MySQL. To set up the database:

1. Install MySQL Community Server
2. Open MySQL Workbench (or any MySQL client) and run the script in "database\\\_setup.sql"
3. Open "TaskRepository.cs" and update the connection string with your own MySQL username/password

## Setup

1. Download or clone the repository
2. Open POEPART2.slnx in Visual Studio
3. Press F5 or click Start to build and run

## Usage Instructions

1. Enter your name in the text field at the top and click Start Chat
2. Type a message in the input box at the bottom and press Enter or click Send
3. Ask about any cybersecurity topic such as:

   * "Tell me about password safety"
   * "I'm worried about online scams"
   * "Give me a phishing tip"
   * "What should I know about privacy?"
4. Simply close the application when you are done

## Youtube Video Link

https://youtu.be/y4kXWuJaGEk

