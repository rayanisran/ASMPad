;a sensitive brick that shatters when mario touches it, but is solid for sprites
;act like 25
JMP MarioTouch : JMP MarioTouch : JMP MarioTouch : JMP SpriteTouch : JMP SpriteTouch : JMP Return : JMP SpriteTouch

MarioTouch:
;shatter
	PHY		;preserve map16 high

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
	
SpriteTouch:
;is solid if a sprite touches
	LDY #$01	;cement
	LDA #$30
	STA $1693

Return:
	RTL