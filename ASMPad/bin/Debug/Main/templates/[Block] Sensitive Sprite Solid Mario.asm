;a sensitive brick that shatters when sprites touch it, but is solid for mario
;act like 25
JMP MarioTouch : JMP MarioTouch : JMP MarioTouch : JMP SpriteTouch : JMP SpriteTouch : JMP Return : JMP SpriteTouch

SpriteTouch:
;shatter
	PHY		;preserve map16 high

	LDA $14E0,x	;write block pos
	XBA
	LDA $E4,x
	REP #$20
	STA $9A
	SEP #$20

	LDA $14D4,x	;write block pos
	XBA
	LDA $D8,x
	REP #$20
	STA $98
	SEP #$20

	PHB		;need to change bank
	LDA #$02
	PHA
	PLB		;to 02
	LDA #$00	;default shatter
	JSL $028663	;shatter block
	PLB		;restore bank

;vanish
	LDA #$02	;erase self
	STA $9C
	JSL $00BEB0	;generate blank block

	PLY		;restore map16 high
	RTL		;just return
	
MarioTouch:
;is solid if a sprite touches
	LDY #$01	;cement
	LDA #$30
	STA $1693

Return:
	RTL