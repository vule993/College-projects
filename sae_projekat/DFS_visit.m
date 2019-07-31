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
## @deftypefn {Function File} {@var{retval} =} DFS_visit (@var{input1}, @var{input2})
##
## @seealso{}
## @end deftypefn

## Author: vukasin <vukasin@vukasin-Inspiron-3542>
## Created: 2017-01-13

function [G] = DFS_visit (G, u,colIndex,obradjeni)
    colors = ['W' 'G' 'B' 'Z' 'P' 'T' 'C' 'L' 'D' 'N'];
  
    G.V(u).color = colors(colIndex);
    G.V(u).boja = colIndex;
    G.V(u).posecen = 1;
    
    % u ovom vektoru naci ce se svi obradjeni(obojeni) cvorovi sve dok ne naidje na ostrvo!
    obradjeni = [obradjeni u];
    
    #plotGraph(G)
    #pause;
    
    %klasican DFS ide u sirinu sve dok ne dodje do kraja  
    for v = find(G.AdjMatrix(u,:))
        if G.V(v).posecen == 0
            %4. parametra      graf, element koji bojimo, vektor obradjenih, i trenutnu boju (funkcija odabira novu boju) 
            colIndex = odluci_koja_boja(G,v,obradjeni,colIndex);
            G.V(v).pred = u;
            G = DFS_visit(G,v,colIndex,obradjeni);           
        end
    end 

    G.V(u).posecen = 1;
    
endfunction
