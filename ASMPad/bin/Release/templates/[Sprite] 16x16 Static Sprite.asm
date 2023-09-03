
dcb "INIT"
RTL

dcb "MAIN"

JSR Graphics 	  ; This routine controls graphics.
RTL		  ; After the graphics routine is over, return.


Graphics:
	JSR GET_DRAW_INFO 	; Before actually coding the graphics, we need a routine that will get the current sprite's value 
				; into the OAM.
				; The OAM being a place where the tile data for the current sprite will be stored.
	LDA $00			;\
	STA $0300,y		;/ Draw the X position of the sprite

	LDA $01			;\
	STA $0301,y		;/ Draw the Y position of the sprite

;Those 2 above are always needed to figure out the X/Y position of the sprite

	LDA #$24		;\
	STA $0302,y		;/ Tile to draw. This is currently tile 24.
				; NOTE: The tile is the upper-left tile of the 16x16 tile you want to draw

	LDA #$06
	ORA $64
	STA $0303,y		; Write the YXPPCCCT property byte of the sprite
				; See this part of the tutorial again for more info

	INY			;\
	INY			; | The OAM is 8x8, but our sprite is 16x16 ..
	INY			; | So increment it 4 times.
	INY			;/

	LDY #$02		; Y ends with the tile size .. 02 means it's 16x16
	LDA #$00		; A -> number of tiles drawn - 1.
				; I drew only 1 tile so I put in 1-1 = 00.

	JSL $01B7B3		; Call the routine that draws the sprite.
	RTS			; Never forget this!

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; GET_DRAW_INFO
; This is a helper for the graphics routine.  It sets off screen flags, and sets up
; variables.  It will return with the following:
;
;       Y = index to sprite OAM ($300)
;       $00 = sprite x position relative to screen boarder
;       $01 = sprite y position relative to screen boarder  
;
; It is adapted from the subroutine at $03B760
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

SPR_T1              dcb $0C,$1C
SPR_T2              dcb $01,$02

GET_DRAW_INFO       STZ $186C,x             ; reset sprite offscreen flag, vertical
	STZ $15A0,x             ; reset sprite offscreen flag, horizontal
	LDA $E4,x               ; \
	CMP $1A                 ;  | set horizontal offscreen if necessary
	LDA $14E0,x             ;  |
	SBC $1B                 ;  |
	BEQ ON_SCREEN_X         ;  |
	INC $15A0,x             ; /

ON_SCREEN_X         LDA $14E0,x             ; \
	XBA	 ;  |
	LDA $E4,x               ;  |
	REP #$20                ;  |
	SEC	 ;  |
	SBC $1A                 ;  | mark sprite invalid if far enough off screen
	CLC	 ;  |
	ADC.W #$0040            ;  |
	CMP.W #$0180            ;  |
	SEP #$20                ;  |
	ROL A                   ;  |
	AND #$01                ;  |
	STA $15C4,x             ; / 
	BNE INVALID             ; 
	
	LDY #$00                ; \ set up loop:
	LDA $1662,x             ;  | 
	AND #$20                ;  | if not smushed (1662 & 0x20), go through loop twice
	BEQ ON_SCREEN_LOOP      ;  | else, go through loop once
	INY	 ; / 
ON_SCREEN_LOOP      LDA $D8,x               ; \ 
	CLC	 ;  | set vertical offscreen if necessary
	ADC SPR_T1,y            ;  |
	PHP	 ;  |
	CMP $1C                 ;  | (vert screen boundry)
	ROL $00                 ;  |
	PLP	 ;  |
	LDA $14D4,x             ;  | 
	ADC #$00                ;  |
	LSR $00                 ;  |
	SBC $1D                 ;  |
	BEQ ON_SCREEN_Y         ;  |
	LDA $186C,x             ;  | (vert offscreen)
	ORA SPR_T2,y            ;  |
	STA $186C,x             ;  |
ON_SCREEN_Y         DEY	 ;  |
	BPL ON_SCREEN_LOOP      ; /

	LDY $15EA,x             ; get offset to sprite OAM
	LDA $E4,x               ; \ 
	SEC	 ;  | 
	SBC $1A                 ;  | $00 = sprite x position relative to screen boarder
	STA $00                 ; / 
	LDA $D8,x               ; \ 
	SEC	 ;  | 
	SBC $1C                 ;  | $01 = sprite y position relative to screen boarder
	STA $01                 ; / 
	RTS	 ; return

INVALID             PLA	 ; \ return from *main gfx routine* subroutine...
	PLA	 ;  |    ...(not just this subroutine)
	RTS	 ; /