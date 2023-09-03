;generates a sprite when touched by Mario with the below parameters
;by smkdan
;Act like whatever
JMP MarioBelow : JMP MarioAbove : JMP MarioSide : JMP SpriteV : JMP SpriteH : JMP MarioCape : JMP MarioFireBall
;don't touch those

;NOTE:
;If you want to spawn a stationary shell, select a shelled koopa of the color you like and set INITSTAT to 09

!SPRITENUMBER = $00	;sprite # to generate
!ISCUSTOM = $00		;set to 01 to generate custom or 00 for standard
!INITSTAT = $01		;change only if the note told you so

!XDISP = $0020		;value added to block position on generation
!YDISP = $FFE0
!XSPD = $00		;speed values
!YSPD = $00

Return_l:
	RTL

MarioAbove: 
MarioSide: 
MarioBelow:
	PHY		;preserve Y
	JSL $02A9DE	;grab sprite slot
	BMI Return_l	;return if none avaliable
	TYX		;into X

	LDA #!INITSTAT	;set to initial status
	STA $14C8,x	;status

	LDA #!ISCUSTOM	;if custom set different type table
	BNE Custom

;Standard:
	LDA #!SPRITENUMBER	;sprite to generate
	STA $9E,x		;sprite type
	JSL $07F7D2		;clear
	BRA SkipCustom
  
Custom:
	LDA #!SPRITENUMBER
	STA $7FAB9E,x	;custom sprite type table
	JSL $07F7D2	;clear
	JSL $0187A7	;load tables
        LDA #$88		;set as custom sprite
        STA $7FAB10,X
		
SkipCustom:
	REP #$20	;apply xdisp
	LDA $9A
	CLC
	ADC #!XDISP
	SEP #$20
	STA $E4,x
	XBA
	STA $14E0,x

	REP #$20	;apply ydisp
	LDA $98
	CLC
	ADC #!YDISP
	SEP #$20
	STA $D8,x
	XBA
	STA $14D4,x

	LDA #!XSPD	;store speeds
	STA $B6,x
	LDA #!YSPD
	STA $AA,x

	LDA #$02	;erase self
	STA $9C
	JSL $00BEB0	;generate blank block

SpriteV: 
SpriteH:
MarioCape: 
MarioFireBall:
	PLY		;restore Y
	RTL