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
## @deftypefn {Function File} {@var{retval} =} count_colors (@var{input1}, @var{input2})
##
## @seealso{}
## @end deftypefn

## Author: vukasin <vukasin@vukasin-Inspiron-3542>
## Created: 2017-01-13

function [] = count_colors (G)

    v = 1:length(G.AdjMatrix);

    fajl = fopen("bojeCvorova.txt","w");
    fajl1 = fopen("raspodelaPoRibnjacima.txt","w");
    
    fprintf(fajl,"%s","redni broj boje  |  broj cvorova \n");
    fprintf(fajl1,"%s","r. br. ribnjaka  |  broj cvorova \n");
    fprintf(fajl,"%s","====================================\n");
    fprintf(fajl1,"%s","====================================\n");
    c = 0; #brojac koliko ima ribnjaka
    for u = v % ovo u jeste samo niz od 1 do 10 jer postoji 10 mogucih boja
        brojac = 0; %koliko ima boja, ili koliko ima riba u 1,2,3,4...ili 10-om ribnjaku 
        
        for k = v
            % zbog ovoga sam uveo numericku predstavu boje, lakse je za operisanje
            if G.V(k).boja == u
                brojac = brojac+1;
            end
        end
        % c je brojac koji govori koliko ima ribnjaka
        if brojac ~= 0  %ako u ribnjaku(brojac konkretne boje ili ribnjaka) nesto ima povecaj broj ribnjaka
            c = c + 1;
        end
        fprintf(fajl,"\t %d\t\t\t | \t\t\t %d \n",u,brojac);
        fprintf(fajl1,"\t %d\t\t\t | \t\t\t %d \n",u,brojac);
        fprintf(fajl,"%s","------------------------------------\n");
    end
    for u = v
        
    end 
    fprintf(fajl1,"\n\n Potrebno je %d ribnjaka da se rasporedi ovih %d riba! \n",c,numel(v));  
    fclose(fajl);
    fclose(fajl1);
endfunction
