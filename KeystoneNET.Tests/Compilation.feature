﻿Feature: Compilation

Scenario: Compile the nop instruction
	Given An instance of Keystone built for X86 in mode 32
	And The statement(s) "nop"
	When I compile the statement(s) with Keystone
	Then the result is 90

Scenario: Compile an invalid instruction
	Given An instance of Keystone built for X86 in mode 32
	And The statement(s) "invalid instruction"
	When I compile the statement(s) with Keystone
	Then The last error is ASM_MNEMONICFAIL

Scenario: Compile a jump to label
	Given An instance of Keystone built for X86 in mode 32
	And The symbols resolver "Standard"
	And The statement(s) "jmp _l;"
	When I compile the statement(s) with Keystone
	Then the result is 90
