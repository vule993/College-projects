## Copyright (C) 2016 vukasin
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
## @deftypefn {Function File} {@var{retval} =} DFS (@var{input1}, @var{input2})
##
## @seealso{}
## @end deftypefn

## Author: vukasin <vukasin@vukasin-Inspiron-3542>
## Created: 2017-01-13

function [G] = DFS (G)
    
    v = 1:length(G.AdjMatrix);
    
    for u = v
        G.V(u).color = 'W'; % boja kao karakter radi iscrtavanja
        G.V(u).boja = 0;    % boja kao broj radi lakseg pronalazenja sledece boje
        G.V(u).pred = NaN;  % prethodnik cvora u pocetku niko nijici (NaN)
        G.V(u).posecen = 0; % indikator da li je cvor posecen, jer belu boju koristimo kao regularnu boju
    end
       
    colorId = 1;
    %morao sam obradjene ovde da def jer DFS_visit zahteva ovaj parametar
    obradjeni = []; #sluzi da pronadje sve posecene (vec obojene), resetuje se ako se dodje do ostrva
    
    for u = v
        if G.V(u).posecen == 0   
 
            u_vezi_sa = find(G.AdjMatrix(u,:));
            %ovaj for sluzi kada dodjemo do ostrva recimo, na njega niko ne pokazuje, a dfs ga pokriva. Boja prvog cvora tog ostrva 
            %ne mora nuzno biti bela ("matrica iz praktikuma") pa ovaj for procesljava sve elemente koji su obojeni i NA KOJE POKAZUJE 
            %PRVI CVOR OSTRVA, prethodne boje ne smemo menjati ali ovu mozemo prilagoti           
            for k = u_vezi_sa
                if G.V(k).posecen == 1
                    % funkcija prima 4. parametra(graf,cvor koji bojimo, cvor koji pokazuje na njega i index boje
                    % napomena: index boje se moze promeniti u funkciji u zavisnosti od slucaja do slucaja
                    colorId = odluci_koja_boja(G,u,NaN,1);
                end
            end
             
            G = DFS_visit(G,u,colorId,obradjeni);
                        
        end
    end
    % kada smo prosli kroz ceo graf i obojili ga protrcavamo ponovo kroz njega kako bismo skupili informacije za txt fajlove!
    count_colors(G);
    
endfunction
