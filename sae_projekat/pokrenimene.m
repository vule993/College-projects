clear;
clc;



#matrica iz praktikuma1
G.AdjMatrix = [
0 1 0 0 0 1 1;
0 0 0 0 0 1 0;
0 0 0 0 0 0 1;
0 0 1 0 1 0 0;
0 0 0 0 0 0 0;
0 0 0 1 1 0 1;
0 0 0 0 1 0 0];    

#proizvoljna matrica
G.AdjMatrix = [
      0 1 0 1 0 0 0 0 0 0;
      0 0 1 1 1 0 0 1 0 1;
      0 0 0 1 0 0 0 0 0 0;
      0 0 1 0 0 0 0 1 0 0;
      0 0 0 0 0 1 0 1 0 0;
      0 0 0 0 0 0 1 0 0 0;
      0 0 0 0 1 0 0 0 0 0;
      0 0 0 1 0 0 1 0 0 0;
      0 0 0 0 1 0 0 0 0 0;
      0 0 0 0 0 0 0 0 1 0;];    


      
#matrica iz praktikuma
G.AdjMatrix = [
0 1 0 1 0 0;
0 0 0 0 1 0;
0 0 0 0 1 1;
0 1 0 0 0 0;
0 0 0 1 0 0;
0 0 0 0 0 1];

ribnjaci

Gp = DFS(G);

plotGraph(Gp);

