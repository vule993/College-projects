## Copyright (C) 2017 vukasin
## 
## This program is free software; you can redistribute it and/or modify it
## under the terms of the GNU General Public License as published by
## the Free Software Foundation; either version 3 of the License, or
## (at your option) any later version.
## 
## This program is distributed in the hope that it will be useful,
## but WITHOUT ANY WARRANTY; without even the implied warranty of
## MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
## GNU General Public License for more details.
## 
## You should have received a copy of the GNU General Public License
## along with this program.  If not, see <http://www.gnu.org/licenses/>.

## -*- texinfo -*- 
## @deftypefn {Function File} {@var{retval} =} odluci_koja_boja (@var{input1}, @var{input2})
##
## @seealso{}
## @end deftypefn

## Author: vukasin <vukasin@vukasin-Inspiron-3542>
## Created: 2017-01-13

function [colIndex] = odluci_koja_boja (G,v,obradjeni,colIndex)
#ovo je slucaj kada se pretrazuje prvo ostrvo i njegovi cvorovi boje
koji = []; # pamtimo koji od prethodnika pokazuju na ovog koji cemo sada bojiti

on_je_sa = find(G.AdjMatrix(v,:));
koji = [koji on_je_sa];

for p = obradjeni   
    tf = G.AdjMatrix(p,v);
    #to znaci da neki(jednina, ali ih moze biti vise) od prethodnika pokazuju na ovaj ukljucujuci i ovaj gore u
    if tf == 1   
        koji = [koji p];
    end
end
#}
            
if ~isempty(koji)
    boje = [];
    for k = koji
        boje = [boje G.V(k).boja];
    end
end
boje = sort(boje);
indikator = 1;

for b = boje
    colIndex = colIndex + 1;
    if indikator ~= b
        colIndex = indikator;  #indikator sluzi da nadje rupu (gde fali broj u nizu) 1-1 2-2 3-4 fali 3-3 znaci boja koju koristimo je 3
    else 
        indikator = indikator + 1;
    end
end


endfunction
