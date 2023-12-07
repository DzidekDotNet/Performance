## .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2
```assembly
; CollectionIndexerLastBenchmark.Last()
       sub       rsp,28
       mov       rdx,[rcx+8]
       lea       r8,[rsp+20]
       mov       rcx,offset MD_System.Linq.Enumerable.TryGetLast[[System.String, System.Private.CoreLib]](System.Collections.Generic.IEnumerable`1<System.String>, Boolean ByRef)
       call      qword ptr [7FF8E75FC840]; System.Linq.Enumerable.TryGetLast[[System.__Canon, System.Private.CoreLib]](System.Collections.Generic.IEnumerable`1<System.__Canon>, Boolean ByRef)
       cmp       byte ptr [rsp+20],0
       je        short M00_L00
       add       rsp,28
       ret
M00_L00:
       call      qword ptr [7FF8E70A4F90]
       int       3
; Total bytes of code 48
```
```assembly
; System.Linq.Enumerable.TryGetLast[[System.__Canon, System.Private.CoreLib]](System.Collections.Generic.IEnumerable`1<System.__Canon>, Boolean ByRef)
       push      rbp
       push      rdi
       push      rsi
       push      rbx
       sub       rsp,48
       lea       rbp,[rsp+60]
       xor       eax,eax
       mov       [rbp-30],rax
       mov       [rbp-40],rsp
       mov       [rbp-20],rcx
       mov       [rbp+20],r8
       mov       rbx,rcx
       mov       rsi,rdx
       test      rsi,rsi
       je        near ptr M01_L17
       mov       rcx,[rbx+10]
       cmp       qword ptr [rcx+8],30
       jle       short M01_L00
       mov       rcx,[rcx+30]
       test      rcx,rcx
       je        short M01_L00
       jmp       short M01_L01
M01_L00:
       mov       rcx,rbx
       mov       rdx,7FF8E76D9858
       call      CORINFO_HELP_RUNTIMEHANDLE_METHOD
       mov       rcx,rax
M01_L01:
       mov       rdx,rsi
       call      qword ptr [7FF8E6EE4348]; System.Runtime.CompilerServices.CastHelpers.IsInstanceOfInterface(Void*, System.Object)
       mov       rdi,rax
       test      rdi,rdi
       jne       near ptr M01_L18
       mov       rcx,[rbx+10]
       cmp       qword ptr [rcx+8],38
       jle       short M01_L02
       mov       rcx,[rcx+38]
       test      rcx,rcx
       je        short M01_L02
       jmp       short M01_L03
M01_L02:
       mov       rcx,rbx
       mov       rdx,7FF8E76D9A48
       call      CORINFO_HELP_RUNTIMEHANDLE_METHOD
       mov       rcx,rax
M01_L03:
       mov       rdx,rsi
       call      qword ptr [7FF8E6EE4348]; System.Runtime.CompilerServices.CastHelpers.IsInstanceOfInterface(Void*, System.Object)
       mov       rdi,rax
       test      rdi,rdi
       je        near ptr M01_L08
       mov       rcx,[rbx+10]
       cmp       qword ptr [rcx+8],50
       jle       short M01_L04
       mov       r11,[rcx+50]
       test      r11,r11
       je        short M01_L04
       jmp       short M01_L05
M01_L04:
       mov       rcx,rbx
       mov       rdx,7FF8E76D9D70
       call      CORINFO_HELP_RUNTIMEHANDLE_METHOD
       mov       r11,rax
M01_L05:
       mov       rcx,rdi
       call      qword ptr [r11]
       mov       esi,eax
       test      esi,esi
       jle       near ptr M01_L15
       mov       rdx,[rbp+20]
       mov       byte ptr [rdx],1
       mov       rcx,[rbx+10]
       cmp       qword ptr [rcx+8],58
       jle       short M01_L06
       mov       r11,[rcx+58]
       test      r11,r11
       je        short M01_L06
       jmp       short M01_L07
M01_L06:
       mov       rcx,rbx
       mov       rdx,7FF8E76D9D88
       call      CORINFO_HELP_RUNTIMEHANDLE_METHOD
       mov       r11,rax
M01_L07:
       lea       edx,[rsi-1]
       mov       rcx,rdi
       call      qword ptr [r11]
       nop
       add       rsp,48
       pop       rbx
       pop       rsi
       pop       rdi
       pop       rbp
       ret
M01_L08:
       mov       rcx,[rbx+10]
       cmp       qword ptr [rcx+8],40
       jle       short M01_L09
       mov       r11,[rcx+40]
       test      r11,r11
       je        short M01_L09
       jmp       short M01_L10
M01_L09:
       mov       rcx,rbx
       mov       rdx,7FF8E76D9B78
       call      CORINFO_HELP_RUNTIMEHANDLE_METHOD
       mov       r11,rax
M01_L10:
       mov       rcx,rsi
       call      qword ptr [r11]
       mov       [rbp-28],rax
       mov       rcx,[rbp-28]
       mov       r11,7FF8E6DA0A68
       call      qword ptr [r11]
       test      eax,eax
       je        short M01_L14
M01_L11:
       mov       rcx,[rbx+10]
       cmp       qword ptr [rcx+8],48
       jle       short M01_L12
       mov       r11,[rcx+48]
       test      r11,r11
       je        short M01_L12
       jmp       short M01_L13
M01_L12:
       mov       rcx,rbx
       mov       rdx,7FF8E76D9B90
       call      CORINFO_HELP_RUNTIMEHANDLE_METHOD
       mov       r11,rax
M01_L13:
       mov       rcx,[rbp-28]
       call      qword ptr [r11]
       mov       rsi,rax
       mov       rcx,[rbp-28]
       mov       r11,7FF8E6DA0A70
       call      qword ptr [r11]
       test      eax,eax
       jne       short M01_L11
       mov       rdx,[rbp+20]
       mov       byte ptr [rdx],1
       mov       [rbp-30],rsi
       mov       rcx,rsp
       call      M01_L21
       jmp       short M01_L16
M01_L14:
       mov       rcx,rsp
       call      M01_L21
       nop
M01_L15:
       mov       rdx,[rbp+20]
       mov       byte ptr [rdx],0
       xor       eax,eax
       add       rsp,48
       pop       rbx
       pop       rsi
       pop       rdi
       pop       rbp
       ret
M01_L16:
       mov       rax,[rbp-30]
       add       rsp,48
       pop       rbx
       pop       rsi
       pop       rdi
       pop       rbp
       ret
M01_L17:
       mov       ecx,10
       call      qword ptr [7FF8E70A4F30]
       int       3
M01_L18:
       mov       rcx,[rbx+10]
       cmp       qword ptr [rcx+8],60
       jle       short M01_L19
       mov       r11,[rcx+60]
       test      r11,r11
       je        short M01_L19
       jmp       short M01_L20
M01_L19:
       mov       rcx,rbx
       mov       rdx,7FF8E76D9DA0
       call      CORINFO_HELP_RUNTIMEHANDLE_METHOD
       mov       r11,rax
M01_L20:
       mov       rcx,rdi
       mov       rdx,[rbp+20]
       call      qword ptr [r11]
       nop
       add       rsp,48
       pop       rbx
       pop       rsi
       pop       rdi
       pop       rbp
       ret
M01_L21:
       push      rbp
       push      rdi
       push      rsi
       push      rbx
       sub       rsp,28
       mov       rbp,[rcx+20]
       mov       [rsp+20],rbp
       lea       rbp,[rbp+60]
       cmp       qword ptr [rbp-28],0
       je        short M01_L22
       mov       rcx,[rbp-28]
       mov       r11,7FF8E6DA0A78
       call      qword ptr [r11]
M01_L22:
       nop
       add       rsp,28
       pop       rbx
       pop       rsi
       pop       rdi
       pop       rbp
       ret
; Total bytes of code 641
```

## .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2
```assembly
; CollectionIndexerLastBenchmark.Indexer()
       push      rbx
       sub       rsp,20
       mov       rcx,[rcx+8]
       mov       rbx,rcx
       mov       rax,offset MT_System.Collections.Generic.List`1[[System.String, System.Private.CoreLib]]
       cmp       [rcx],rax
       jne       short M00_L00
       mov       eax,[rcx+10]
       lea       edx,[rax-1]
       cmp       edx,eax
       jae       short M00_L01
       mov       rax,[rbx+8]
       cmp       edx,[rax+8]
       jae       short M00_L03
       mov       ecx,edx
       mov       rax,[rax+rcx*8+10]
       add       rsp,20
       pop       rbx
       ret
M00_L00:
       mov       r11,7FF8E6DD09C8
       call      qword ptr [r11]
       lea       edx,[rax-1]
       jmp       short M00_L02
M00_L01:
       call      qword ptr [7FF8E70AEA30]
       int       3
M00_L02:
       mov       rcx,rbx
       mov       r11,7FF8E6DD09D0
       cmp       [rcx],ecx
       add       rsp,20
       pop       rbx
       jmp       qword ptr [r11]
M00_L03:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 113
```

## .NET 8.0.0 (8.0.23.47906), X64 RyuJIT AVX2
```assembly
; CollectionIndexerLastBenchmark.ElementAt()
       sub       rsp,28
       mov       rdx,[rcx+8]
       mov       r8d,0FFFFFFFE
       mov       rcx,offset MD_System.Linq.Enumerable.ElementAt[[System.String, System.Private.CoreLib]](System.Collections.Generic.IEnumerable`1<System.String>, System.Index)
       call      qword ptr [7FF8E75FDED8]; System.Linq.Enumerable.ElementAt[[System.__Canon, System.Private.CoreLib]](System.Collections.Generic.IEnumerable`1<System.__Canon>, System.Index)
       nop
       add       rsp,28
       ret
; Total bytes of code 36
```
```assembly
; System.Linq.Enumerable.ElementAt[[System.__Canon, System.Private.CoreLib]](System.Collections.Generic.IEnumerable`1<System.__Canon>, System.Index)
       push      rdi
       push      rsi
       push      rbp
       push      rbx
       sub       rsp,38
       xor       eax,eax
       mov       [rsp+20],rax
       mov       [rsp+30],rcx
       mov       [rsp+70],r8
       mov       rbx,rcx
       mov       rsi,rdx
       test      rsi,rsi
       je        near ptr M01_L08
       cmp       dword ptr [rsp+70],0
       jge       near ptr M01_L09
       mov       rcx,[rbx+10]
       mov       rcx,[rcx+20]
       test      rcx,rcx
       je        short M01_L03
M01_L00:
       lea       r8,[rsp+28]
       mov       rdx,rsi
       call      qword ptr [7FF8E75FDF08]; System.Linq.Enumerable.TryGetNonEnumeratedCount[[System.__Canon, System.Private.CoreLib]](System.Collections.Generic.IEnumerable`1<System.__Canon>, Int32 ByRef)
       test      eax,eax
       je        short M01_L04
       mov       edi,[rsp+28]
       mov       ecx,[rsp+70]
       mov       ebp,ecx
       not       ebp
       cmp       dword ptr [rsp+70],0
       cmovge    ebp,[rsp+70]
       mov       rcx,[rbx+10]
       cmp       qword ptr [rcx+8],30
       jle       short M01_L02
       mov       rcx,[rcx+30]
       test      rcx,rcx
       je        short M01_L02
M01_L01:
       mov       r8d,edi
       sub       r8d,ebp
       mov       rdx,rsi
       call      qword ptr [7FF8E75FDF38]; System.Linq.Enumerable.ElementAt[[System.__Canon, System.Private.CoreLib]](System.Collections.Generic.IEnumerable`1<System.__Canon>, Int32)
       nop
       add       rsp,38
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M01_L02:
       mov       rcx,rbx
       mov       rdx,7FF8E76D9D58
       call      CORINFO_HELP_RUNTIMEHANDLE_METHOD
       mov       rcx,rax
       jmp       short M01_L01
M01_L03:
       mov       rcx,rbx
       mov       rdx,7FF8E76D9BD0
       call      CORINFO_HELP_RUNTIMEHANDLE_METHOD
       mov       rcx,rax
       jmp       near ptr M01_L00
M01_L04:
       mov       rcx,[rbx+10]
       mov       rdi,[rcx+28]
       test      rdi,rdi
       je        short M01_L05
       jmp       short M01_L06
M01_L05:
       mov       rcx,rbx
       mov       rdx,7FF8E76D9C80
       call      CORINFO_HELP_RUNTIMEHANDLE_METHOD
       mov       rdi,rax
M01_L06:
       lea       rcx,[rsp+70]
       call      qword ptr [7FF8E72C6B20]; System.Index.get_Value()
       mov       r8d,eax
       mov       rcx,rdi
       lea       r9,[rsp+20]
       mov       rdx,rsi
       call      qword ptr [7FF8E76EC8B8]
       test      eax,eax
       jne       short M01_L07
       mov       ecx,6
       call      qword ptr [7FF8E70A4F48]
       int       3
M01_L07:
       mov       rax,[rsp+20]
       add       rsp,38
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
M01_L08:
       mov       ecx,10
       call      qword ptr [7FF8E70A4F30]
       int       3
M01_L09:
       mov       rcx,[rbx+10]
       cmp       qword ptr [rcx+8],30
       jle       short M01_L10
       mov       rdi,[rcx+30]
       test      rdi,rdi
       je        short M01_L10
       jmp       short M01_L11
M01_L10:
       mov       rcx,rbx
       mov       rdx,7FF8E76D9D58
       call      CORINFO_HELP_RUNTIMEHANDLE_METHOD
       mov       rdi,rax
M01_L11:
       lea       rcx,[rsp+70]
       call      qword ptr [7FF8E72C6B20]; System.Index.get_Value()
       mov       r8d,eax
       mov       rcx,rdi
       mov       rdx,rsi
       call      qword ptr [7FF8E75FDF38]; System.Linq.Enumerable.ElementAt[[System.__Canon, System.Private.CoreLib]](System.Collections.Generic.IEnumerable`1<System.__Canon>, Int32)
       nop
       add       rsp,38
       pop       rbx
       pop       rbp
       pop       rsi
       pop       rdi
       ret
; Total bytes of code 386
```

