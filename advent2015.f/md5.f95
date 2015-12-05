! MD5
! Posted by onkelhotte, May 7 2010
! https://software.intel.com/en-us/forums/intel-visual-fortran-compiler-for-windows/topic/274000
! 
! Hacked to make work with Sun Fortran by Ross Nelson, Dec 4 2015
! https://github.com/rnelson/adventofcode/blob/master/advent2015.f/md5.f95

! **********************************************************************
character*32 function md5(string)
! ---------------------------------------------------------------------*
!     Programmierer    : VEZ2/Pieper                                   *
!     Version          : 1.0                                           *
!     letzte nderung  : 07.05.2010                                    *
!     Aufgabe          : Erzeugt aus einem String einen MD5 Hashwert   *
! **********************************************************************

implicit none

character*(*) string
character*((int(len(string)/64)+1)*64) newString
character*8 wtmp

integer(kind=4) j,n1,n2,n3,n4,umdrehen,pos
integer(kind=4) r(64),k(64),h0,h1,h2,h3,a,b,c,d,f,g,temp,w(16),leftrotate,i,intLen
integer(kind=8) hoch32
real(kind=8) sinus,absolut,real8i

r = [7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22, 5,  9, 14, 20, &
    5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20, 4, 11, 16, 23,  4, 11, 16, 23,  &
    4, 11, 16, 23,  4, 11, 16, 23, 6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,  &
    6, 10, 15, 21]

do i=1,64
    real8i = floatk(int(i, 8))
    sinus = dsin(real8i)
    absolut = dabs(sinus)
    hoch32 = 2.**32.
    k(i) = int(absolut * floatk(hoch32), 8)
end do

h0 = Z'67452301'
h1 = Z'EFCDAB89'
h2 = Z'98BADCFE'
h3 = Z'10325476'

j = len(string)+1
newString(:j) = string // char(128)
i = mod(j, 64)
do while(i /= 56)
    j = j + 1
    newString(j:j) = char(0)
    i = mod(j, 64)
end do

intLen = len(string)*8
do i = 0,3
    temp = intLen .and. Z'FF'
    j = j + 1
    newString(j:j) = char(temp)
    intLen = shiftr(intLen, 8)
end do

do i = 1,4
    j = j + 1
    newString(j:j) = char(0)
end do

do i = 1,int(len(newString)/64)

    do j = 1,16
        pos = (j-1)*4+(i-1)*64
        n1 = ichar(newString(4+pos:4+pos))
        n2 = ichar(newString(3+pos:3+pos))
        n3 = ichar(newString(2+pos:2+pos))
        n4 = ichar(newString(1+pos:1+pos))
        
        write(wtmp,'(4(z2.2))') n1,n2,n3,n4
        read(wtmp,'(z8)') w(j)
    end do

    a = h0
    b = h1
    c = h2
    d = h3

    do j = 1,64
        if (j >= 1 .and. j <= 16) then
            f = (b .and. c) .or. ((.not. b) .and. d)
            g = j
        else if (j >= 17 .and. j <= 32) then
            f = (d .and. b) .or. ((.not. d) .and. c)
            g = mod(5*(j-1) + 1, 16) + 1
        else if (j >= 33 .and. j <= 48) then
            f = ieor(b, ieor(c, d))
            g = mod(3*(j-1) + 5, 16) + 1
        else if (j >= 49 .and. j <= 64) then
            f = ieor(c, (b .or. (.not. d)))
            g = mod(7*(j-1), 16) + 1
        end if
        
        temp = d
        d = c
        c = b
        b = b + leftrotate((a + f + k(j) + w(g)) , r(j))
        a = temp
    end do

    h0 = h0 + a
    h1 = h1 + b
    h2 = h2 + c
    h3 = h3 + d
end do
h0 = umdrehen(h0)
h1 = umdrehen(h1)
h2 = umdrehen(h2)
h3 = umdrehen(h3)

write(md5,'(4(z8))') h0,h1,h2,h3
return

end function md5
!
!
! **********************************************************************
integer(kind=4) function leftrotate (x, c)
! ---------------------------------------------------------------------*
!     Programmierer    : VEZ2/Pieper                                   *
!     Version          : 1.0                                           *
!     letzte nderung  : 07.05.2010                                    *
!     Aufgabe          : Fhrt ein Leftrotate der Bits durch           *
! **********************************************************************

implicit none

integer(kind=4) x,c,result1,result2

result1 = shiftl(x,c)
result2 = shiftr(x, (32-c))

leftrotate = result1 .or. result2

return
end function leftrotate
!
!
! **********************************************************************
integer(kind=4) function umdrehen(zahl)
! ---------------------------------------------------------------------*
!     Programmierer    : VEZ2/Pieper                                   *
!     Version          : 1.0                                           *
!     letzte nderung  : 07.05.2010                                    *
!     Aufgabe          : Macht aus Big Endian -> Little Endian Bits    *
! **********************************************************************

implicit none

integer(kind=4) i,tmp,zahl

umdrehen = 0
do i = 1,4
    umdrehen = shiftl(umdrehen, 8)
    tmp = zahl .and. Z'FF'
    umdrehen = umdrehen + tmp;
    zahl = shiftr(zahl, 8)
end do

return
end function umdrehen
