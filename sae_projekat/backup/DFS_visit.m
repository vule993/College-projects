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
## Created: 2016-12-29

function [G] = DFS_visit (G, u,colIndex,obradjeni)
    colors = ['W' 'G' 'B' 'Z' 'P' 'T' 'C' 'L' 'D' 'N'];
    #{
    global time;
    time = time + 1;
    #}   
    G.V(u).color = colors(colIndex);
    G.V(u).boja = colIndex;
    G.V(u).posecen = 1;
    #G.V(u).d = time;
    
    obradjeni = [obradjeni u];
    #plotGraph(G)
    #pause;
      
    for v = find(G.AdjMatrix(u,:))
        if G.V(v).color == 'W' && G.V(v).posecen == 0
            %colIndex = colIndex + 1;
            colIndex = odluci_koja_boja(G,v,obradjeni,colIndex);
            G.V(v).pred = u;
            G = DFS_visit(G,v,colIndex,obradjeni);
        end
    end 
    #plotGraph(G)
    #pause;
    %G.V(u).color = 'B';
    G.V(u).posecen = 1;
    #{
    time = time + 1;
    G.V(u).f = time;
    #}
endfunction
