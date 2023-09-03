=================
ASMPad, v1.1
By Iceyoshi
================

ASMPad is an IDE or ASM Editor for Super Mario World. I started the project sometime in 2009 but later discarded the project and became inactive on SMWC. Then I started to work on the project once again in September 2010 and an initial release was made in C3 in November of the same year. Unfortunately, it's written in C# which means that it will only work on Windows and you will need the .NET Framework (which you should have anyway). Note that it's not a big project, so it doesn't contain any tile editors/palette editors/etc. It does contain a hex-editor and some other useful features though.

========
Features
========

-Embedded ROM/RAM Maps, command prompt and hex editor. The hex editor doesn't revert changes to files like translhexation does and it can show and jump to SNES addresses too.
-Multi-file support (tabbed interface).
-Customizable syntax highlighting, tooltips, templates and code snippets. Already included is
-ASM/IPS patching with backup ability. Freespacing logging also becomes easy and you can also insert sprites into a ROM too.
-Code completion for definitions which can be from a text file, the current document or both.
-Macro recording and playback, bookmarks, ability to comment/uncomment code regions and more.
-Assemble file function which reports all errors, highlights the lines and shows a tooltip when those lines are clicked much like Visual Studio.
-Utilities such as SNES/PC address conversion, easy searching of opcodes, sound effects and player poses.
-And much more.

=======
Toolkit
=======

Can do the following:

-ASM patching: 		-a rom.smc patch.asm
-IPS patching: 		-i dirty.ips clean.smc
-Comment removing:	-c file.asm
-freespace logging:	-f rom.smc

=======
Licence
=======

You can use this software freely, provided you distribute this document along with it. I will not be held responsible if this software causes any damage to
your computer, directly or otherwise, in any way.
