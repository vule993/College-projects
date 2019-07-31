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
% obradjeni je vektor u kome se nalaze id-evi cvorova koji su obojeni i poseceni! popunjava se u DFS_visit-u!
% obradjeni
koji = []; 
% pamtimo koji od prethodnika pokazuju na ovog koji cemo sada bojiti ali i one na koje on sam pokazuje (42. linija for petljom)
% primer 4. cvor pri pozivu "matrice iz praktikuma"-> jer njega kada bojimo moramo imati u vidu i na koga on pokazuje a i
% ko na njega pokazuje kako ne bismo duplirali boje!

# u matrici susedstva pronalazimo el. koji bojimo i citamo na koga on pokazuje i to je zapravo vektor koji
on_je_sa = find(G.AdjMatrix(v,:));
koji = [koji on_je_sa];

% one cvorove koji su obradjeni (ako ih ima) ispitujemo da li pokazuju na tekuci cvor, cvor koji tek treba da obojimo! Ako
% pokazuju i njih cemo dodati u vektor koji
% a onda ovde cesljamo obradjene, pa u vektor koji (sadrzi sledbenike tekuceg cvora) dodajemo ONE KOJI NA TEKUCI POKAZUJU
% pa tek AKO SU OBOJENI(58 linija koda) uzimamo im boju u vektor iskoriscenih boja pa uz pomoc indikatora ustanovljavamo 
% koju boju cemo koristiti.
if ~isnan(obradjeni)
    for p = obradjeni  
        %tf = true/false indikator
        % proveravamo u matrici da li NEKI od OBRADJENIH pokazuju na ovaj cvor koji sada bojimo
        tf = G.AdjMatrix(p,v);
        #to znaci da neki(jednina, ali ih moze biti vise) od prethodnika pokazuju na ovaj ukljucujuci i ovaj gore u
        if tf == 1   
            koji = [koji p];
        end
        
    end
end

% prolazimo kroz sve cvorove koji imaju veze sa ovim kojeg sada bojimo i sve te boje izdvajamo u vektor boja            
if ~isempty(koji)
    boje = [];
    for k = koji
        if G.V(k).posecen == 1  % ovaj if nam garantuje da ce se posmatrati samo one boje, pa i bele koje NISU posecene
            boje = [boje G.V(k).boja];
        end
    end
end

boje = sort(boje);  % npr. 1 2 4 5 ( koristi ovaj primer da objasnis )
indikator = 1;

for b = boje
    colIndex = colIndex + 1; % posto smo poslali kao parametar tekucu boju prelazimo na sledecu (ako je uslov ispunjen pregazicemo je)
    if indikator ~= b
        colIndex = indikator;  % indikator sluzi da nadje rupu (gde fali broj u nizu) 1-1 2-2 3-4 fali 3-3 znaci boja koju koristimo je 3
    else 
        indikator = indikator + 1;
    end
end

endfunction
