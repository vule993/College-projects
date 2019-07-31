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
## Created: 2016-12-29

function [G] = DFS (G)
    
    global time;    
    v = 1:length(G.AdjMatrix);
    for u = v
        G.V(u).color = 'W';
        G.V(u).boja = 0;
        G.V(u).pred = NaN;
        G.V(u).posecen = 0;
    end
    time = 0;
    colorId = 1;
    obradjeni = [];
    for u = v
        if G.V(u).color == 'W' && G.V(u).posecen == 0   
 
            u_vezi_sa = find(G.AdjMatrix(u,:));
            for k = u_vezi_sa
                if G.V(k).posecen == 1
                    if G.V(k).boja == 1
                        colorId = 2;
                    end
                end
            end
             
            G = DFS_visit(G,u,colorId,obradjeni);
            colorId = colorId + 1;
        end
    end
    
    
endfunction
