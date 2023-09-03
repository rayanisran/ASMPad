==================
ASMPad, v1.1
By Iceyoshi
24 December 2010
==================

ASMPad is an ASM IDE I made specifically for Super Mario World. By that I mean it has many features specifically for SMW, such as embedded ROM/RAM Maps and an ability to search for Mario poses, sound effects and opcodes. I started the project sometime in 2009 but later discarded the project and became inactive, but started working on it in September 2010. Unfortunately, it's written in C# which means that it's not cross-platform and requires .NET Framework. You can download that from http://www.microsoft.com/downloads/en/details.aspx?FamilyID=9cfb2d51-5ff4-4491-b0e5-b386f32c0992&displaylang=en.

In this ZIP File
================
- readme.txt	- This file.
- RAM/ROM.htm	- ROM/RAM Maps.
- lib.dll	- External libary used by this tool for some components.
- ASMPad.exe	- The tool itself.
- Snippets.XML	- Snippets file.
- def.txt	- A file which you can use to autocomplete definitions. It has 95 definitions added already.

Put all of these in your hacking folder. It's important they're placed there. Also make sure you have xkas, TRASM and slogger.

Features
========

There are plenty of features in the current release. Some of them include:

- File assembling with error reporting ability, which much like Visual Studio displays all errors in a grid and shows the error when they're hovered over.
- Configurable syntax highlighting, templates and snippets which can be inserted with the click of a button.
- Utilities such as Color Conversion, a PC/SNES converter, ASCII2Hex tool and more.
- Many input assistant features such as an auto-completion list that pops up for definitions.
- Embedded ROM and RAM Maps, a built-in hex editor and command prompt. The hex editor unlike Translhexation doesn't revert changes when the file is saved.
- ASM patching/IPS patching/sprite inserting/freespace logging with backup ability and ability to play the ROM in an emulator by pressing F4, and debugger by F3.
- Tooltips for definitions, tables, labels, all opcodes and values. Currently working on adding tooltips for RAM/ROM Addresses also.
- And much more.

Here's a screenshot: http://bin.smwcentral.net/35385/2.png.

More is planned including a help file, so please let me know if you want features to be added to the application.

Things you should know
======================

All snippets are stored in an XML file called Snippets.XML. If the file isn't there they won't be loaded. You can insert a snippet by pressing Ctrl+J or by clicking a snippet from the templates window on the side pane. You can add a snippet manually through the ASM file, or simply by writing a code snippet in the document, selecting it and clicking 'Add To Snippets' on the right-click menu. In the templates window you can right-click a snippet to rename, edit or delete it from the XML file.
For auto-completion, you can choose to load definitions which are in the active file, or choose from a file in the application's directory (should be called def.txt) or from both of them, or you can just disable it if you don't like the feature. If you're loading a definition from def.txt, the format is this:

Address = $010A
Test = $010C

Note that there's no ! before the words, when you type ! then there's no point in adding it again. The def.txt file included in the ZIP contains about 95 definitions already added.

Another thing is that this tool is designed for xkas syntax, meaning it won't assemble files with TRASM (except .BIN exporting), won't recognize TRASM labels/definitions and TRASM syntax. Because it's a garbage assembler and I never intended the tool be to designed for that.

Lastly, you can associate .ASM file types with this application so that they load with ASMPad when double clicked on, but keep in mind that I've never tested this tool that much and haven't really bothered to test it so there probably are some bugs/errors. Let me know about them and I'll get them fixed.

Credits
========

SWR - for helping me and motivating me to work on this.
Noobish Noobsicle - I used some of his routines including the IPS patching one.
smkdan - slogger.
Sourceforge/ScintillaNET - I used their components including the hex-editor and docking panel.
Me - for coding the tool.