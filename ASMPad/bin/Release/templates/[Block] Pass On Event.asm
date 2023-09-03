;Mario can only pass if the specified event has been completed
;by smkdan
;Act like tile 25
JMP MarioBelow : JMP MarioAbove : JMP MarioSide : JMP SpriteV : JMP SpriteH : JMP MarioCape : JMP MarioFireBall
;don't touch those

;!THE EVENT YOU WANT TO TEST FOR!
!EVENT = $01

Mask:
	db $80,$40,$20,$10,$08,$04,$02,$01
	
MarioAbove: 
MarioSide: 
MarioBelow:
	LDA #!EVENT	;load event
	AND #$07	;low 3 bits = mask index
	TAY		;into Y
	LDA #!EVENT	;load event again
	LSR A
	LSR A
	LSR A		;get byte index
	TAX		;into X
	LDA $1F02,x	;load byte
	AND Mask,y	;apply mask
	;EOR Mask,y	;uncomment this to invert the effect (pass on uncleared event)
	BNE EventPassed

;event not passed
	LDY #$01	;act like 130
	LDA #$30
	STA $1693
	RTL		;just return here

EventPassed:
	LDY #$00	;remain tile 25	
	
SpriteV: 
SpriteH:
MarioCape: 
MarioFireBall:
	RTL