#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <windows.h>
void continuar(){
    char val;
    printf("Presione cualquier tecla para continuar\n");
    getch(val);
    system("cls");
}
void imprimirTablero(char arreglo[9][9]){//Imprime el tablero con los disparos
    printf("Y X 0   1   2   3   4   5   6   7   8\n");
    printf("  -------------------------------------\n");
    for (int i = 0;i < 9; ++i){
        printf("%i ",i);
        for (int j = 0; j < 9; ++j){
            printf("| %c ",arreglo[i][j]);
        }
        printf("|\n");
        printf("  -------------------------------------\n");
    }
}
void imprimirHTablero(int arreglo[9][9]){//Imprime el tablero con los barcos
    printf("Y X 0   1   2   3   4   5   6   7   8\n");
    printf("  -------------------------------------\n");
    for (int i = 0;i < 9; ++i){
        printf("%i ",i);
        for (int j = 0; j < 9; ++j){
            printf("| %i ",arreglo[i][j]);
        }
        printf("|\n");
        printf("  -------------------------------------\n");
    }
}
void shipStart(int ship,int board[9][9],int shipSize){//Coloca el barco en las cordenadas indicadas
    int end = 0,i,x,y,orientacion,valPos = 0;
    do{
        imprimirHTablero(board);
        printf("\n\nIntroduce las cordenadas para colocar un barco de %i unidades en el orden X, de enter y luego introduce Y\n",shipSize);
        scanf("%i",&x);
        scanf("%i",&y);
        if (x>=0 && x<9){//Verifica la cordenanda en X
            if (y>=0 && y<9){//Verifica la cordenada en Y
                if (board[y][x] == 0){//Verifica que no halla un barco en el lugar
                    printf("Seleccione la orientacion del barco\n1. Vertical\n2. Horizontal\n");
                    scanf("%i",&orientacion);
                    switch(orientacion){
                    case 1:
                        if (y + shipSize-1 < 9){//Verifica que el barco cabe en el espacio de forma vertical hacia abajo
                            valPos = 0;
                            for (i = 0; i <= shipSize - 1;++i){//Verifica que el barco no se superponga con otro barco
                                if (board[y+i][x] != 0){
                                    valPos = 1;
                                }
                            }
                            if(valPos == 1){
                                printf("Error, no pueden haber mas de 2 barcos en el mismo lugar\n");
                            }else{
                                for(i = 0;i <= shipSize-1;++i){//Coloca el barco
                                    board[y+i][x] = ship;
                                }
                                printf("El barco se coloco exitosamente\n");
                                end = 1;
                            }
                        }else{
                            printf("Error, el barco sale del area de juego\n");
                        }
                        break;
                    case 2:
                        if (x + shipSize-1 < 9){//Verifica que el barco cabe en el espacio de forma horizontal hacia la derecha
                            valPos = 0;
                            for (i = 0; i <= shipSize - 1;++i){//Verifica que el barco no se superponga con otro barco
                                if (board[y][x+i] != 0){
                                    valPos = 1;
                                }
                            }
                            if(valPos == 1){
                                printf("Error, no pueden haber mas de 2 barcos en el mismo lugar\n");
                            }else{
                                for(i = 0;i <= shipSize-1;++i){//Coloca el barco
                                    board[y][x+i] = ship;
                                }
                                printf("El barco se coloco exitosamente\n");
                                end = 1;
                            }
                        }else{
                            printf("Error, el barco sale del area de juego\n");
                        }
                        break;
                    default:
                        printf("Error, por favor seleccione una opcion valida\n");
                        break;
                    }
                }else{
                    printf("Error, ya hay un barco en esa cordenada\n");
                }
            }else{
                printf("Error, por favor introduzca cordenadas validas\n");
            }
        }else{
            printf("Error, por favor introduzca cordenadas validas\n");
        }
        Sleep(1500);
        system("cls");
    }while (end == 0);
}
void Inicio(){
    system("cls");
    int J1Hmap [9][9];
    int J2Hmap [9][9];
    char J1map [9][9];
    char J2map [9][9];
    int shpX1= 1, shpX2 = 2, shpX3 = 3, shpX4 = 4, shpX5 = 5;
    int shpY1= 1, shpY2 = 2, shpY3 = 3, shpY4 = 4, shpY5 = 5;
    int endGame = 0,turn = 0;
    for (int i = 0;i<9;++i){
        for(int j = 0;j<9;++j){//Inicializa los arreglos
            J1map[i][j] = '.';
            J2map[i][j] = '.';
            J1Hmap[i][j] = 0;
            J2Hmap[i][j] = 0;
        }
    }
    shipStart(shpX1,J1Hmap,2);//Se colocan los barcos del jugador 1
    shipStart(shpX2,J1Hmap,3);
    shipStart(shpX3,J1Hmap,3);
    shipStart(shpX4,J1Hmap,4);
    shipStart(shpX5,J1Hmap,5);
    continuar();
    shipStart(shpY1,J2Hmap,2);//Se colocan los barcos del jugador 2
    shipStart(shpY2,J2Hmap,3);
    shipStart(shpY3,J2Hmap,3);
    shipStart(shpY4,J2Hmap,4);
    shipStart(shpY5,J2Hmap,5);
    do{
        turn++;
        if (turn%2 == 0){//Se determina de quien es el turno
            printf("Turno %i: Jugador 2\n\n\n\n",turn);
            continuar();
            disparar(J1map,J1Hmap);
            if(turn > 33){
                endGame = winCondition(J1Hmap);//Analiza si se cumple la condicion de victoria
            }
        }else{
            printf("Turno %i: Jugador 1\n\n\n\n",turn);
            continuar();
            disparar(J2map,J2Hmap);
            if(turn > 32){
                endGame = winCondition(J2Hmap);//Analiza si se cumple la condicion de victoria
            }
        }
        continuar();
    }while (endGame == 0);
    system("cls");
    if (turn%2 == 0){
        printf("FELICIDADES JUGADOR 2, GANASTE\n\n\n");
    }else{
        printf("FELICIDADES JUGADOR 1, GANASTE\n\n\n");
    }
    printf("Tableros del jugador 1\n");
    imprimirTablero(J1map);
    imprimirHTablero(J1Hmap);
    printf("\n\n\nTableros del jugador 2\n");
    imprimirTablero(J2map);
    imprimirHTablero(J2Hmap);
}
void disparar(char visible[9][9], int hidden[9][9]){
    int x,y,flag = 0, fShip = 0,save;
    do{//Realiza el disparo
        imprimirTablero(visible);
        printf("\n\n\nSeleccione la ubicacion del disparo ingrasando primero X, presione enter y luego ingrese Y\n");
        scanf("%i",&x);
        scanf("%i",&y);
        if (x>=0 && x<9){
            if (y>=0 && y<9){//Verifica que las coordenadas sean validas
                if (visible[y][x] == '.'){
                    flag = 1;//Quita el lock del Do
                    if(hidden[y][x] == 0){//Analiza si el disparo fue o no certero
                        printf("\n\nDisparo fallido\n\n");
                        visible[y][x] = 'X';
                        imprimirTablero(visible);
                    }else{
                        fShip = 0;
                        save = hidden[y][x];//Guarda el valor del barco para buscar si fue destruido
                        visible[y][x] = 'O';
                        hidden[y][x] = 0;
                        for(int i = 0;i<9;++i){
                            for(int j = 0;j<9;++j){
                                if (hidden[i][j] == save){
                                    fShip = 1;//Verifica si el barco fue destruido buscando si existe un valor identico del barco que recibio el disparo
                                }
                            }
                        }
                        if (fShip == 1){//
                            printf("\n\nBarco Impactado\n\n");
                            imprimirTablero(visible);
                        }else{//Determina el mensaje si el barco fue destruido o no
                            printf("\n\nBarco Destruido\n\n");
                            imprimirTablero(visible);
                        }
                    }
                }else{//mensajes de error y el reset de la operacion
                    printf("Error, Ya se ha disparado en esa ubicacion\n");
                    Sleep(2000);
                    system("cls");
                }
            }else{
                printf("Porfavor introduzca cordenadas validas\n");
                Sleep(2000);
                system("cls");
            }
        }else{
            printf("Porfavor introduzca cordenadas validas\n");
            Sleep(2000);
            system("cls");
        }
    }while (flag == 0);
}
int winCondition(int field[9][9]){
    for (int i = 0;i < 9;++i){
        for(int j = 0;j < 9;++j){
            if(field[i][j] != 0){
                return 0;//Si encuentra algun barco manda negativo
            }
        }
    }
    return 1;
}
void Instrucciones(){
    system("cls");
    printf("Battleship es un juego de 2 jugadores por turnos en el que los jugadores se turnan para\n");
    printf("destruir los barcos del contrincante. El objetivo es destruir los 5 barcos del rival\n");
    printf("Los tamanios de los barcos son de 2, 3, 3, 4 y 5, cada jugador colocara los barcos en un mapa\n");
    printf("como se muestra a continuacion:\n\n\n");
    printf("Y X 0   1   2   3   4   5   6   7   8\n");
    printf("  -------------------------------------\n");
    printf("0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("2 | 0 | 0 | 0 | 5 | 5 | 5 | 5 | 5 | 0\n");
    printf("  -------------------------------------\n");
    printf("3 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("4 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("5 | 0 | 0 | 0 | 0 | 2 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("6 | 3 | 3 | 3 | 0 | 2 | 0 | 0 | 1 | 1\n");
    printf("  -------------------------------------\n");
    printf("7 | 0 | 0 | 0 | 0 | 2 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("8 | 0 | 0 | 4 | 4 | 4 | 4 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("\n\n\nCada numero representa la ubicacion de un barco, el '0' representa un espacio vacio\n");
    printf("del mar, el '1' representa el barco de tamanio 2, los numeros '2' y '3' representan los barcos\n");
    printf("de tamanio 3, y los ultimos 2 numeros representan los barcos de su respectivo tamanio.\n");
    continuar();
    printf("Para colocar un barco se establecen las cordenadas X y Y del origen y luego la direccion si es\n");
    printf("horizontal o vertical, tomando en cuenta que el seleccionar horizontal se colocara hacia la derecha\n");
    printf("y el vertical hacia abajo del origen, por ejemplo queremos colo car el barco de tamanio 4 en las cordenadas (4,3) a (4,6).\n");
    printf("Para colocarlo se ingresaria de la siguiente forma\n");
    printf("Y X 0   1   2   3   4   5   6   7   8\n");
    printf("  -------------------------------------\n");
    printf("0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("2 | 0 | 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("3 | 0 | 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("4 | 0 | 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("5 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("6 | 3 | 3 | 3 | 0 | 0 | 0 | 0 | 1 | 1\n");
    printf("  -------------------------------------\n");
    printf("7 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("8 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n\n");
    printf("Introduce las coordenadas para colocar un barco de 4 unidades en el orden X, de enter y luego introduce Y\n");
    printf("4\n");
    printf("3\n");
    printf("Seleccione la orientacion del barco\n1. Vertical\n2. Horizontal\n");
    printf("1\n");
    printf("Y X 0   1   2   3   4   5   6   7   8\n");
    printf("  -------------------------------------\n");
    printf("0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("2 | 0 | 2 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("3 | 0 | 2 | 0 | 0 | 4 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("4 | 0 | 2 | 0 | 0 | 4 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("5 | 0 | 0 | 0 | 0 | 4 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("6 | 3 | 3 | 3 | 0 | 4 | 0 | 0 | 1 | 1\n");
    printf("  -------------------------------------\n");
    printf("7 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n");
    printf("8 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0\n");
    printf("  -------------------------------------\n\n");
    printf("Asi se colocaria ese barco en el mapa.\n");
    continuar();
    printf("Para realizar un disparo, se tienen que introducir las coordenadas de la misma forma en la que se coloca un barco,\n");
    printf("El mapa que se mostrara tendra los simbolos '.', 'X' y 'O', el '.' representa un espacio del mar en el que aun no se\n");
    printf("ha realizado ningun disparo, la 'X' representa un lugar donde se realizo un disparo y no habia algun barco, y por ultimo\n");
    printf("la 'O' representa un lugar donde se impacto un barco como se muestra en el mapa:\n");
    printf("Y X 0   1   2   3   4   5   6   7   8\n");
    printf("  -------------------------------------\n");
    printf("0 | . | . | . | . | . | . | . | . | .\n");
    printf("  -------------------------------------\n");
    printf("1 | . | . | . | . | . | . | . | . | .\n");
    printf("  -------------------------------------\n");
    printf("2 | . | . | . | . | . | . | . | . | .\n");
    printf("  -------------------------------------\n");
    printf("3 | . | . | X | . | O | O | O | X | .\n");
    printf("  -------------------------------------\n");
    printf("4 | . | . | . | . | . | . | . | . | .\n");
    printf("  -------------------------------------\n");
    printf("5 | . | . | . | . | . | . | . | . | .\n");
    printf("  -------------------------------------\n");
    printf("6 | . | . | . | . | . | . | . | . | X\n");
    printf("  -------------------------------------\n");
    printf("7 | . | X | . | X | . | . | . | . | .\n");
    printf("  -------------------------------------\n");
    printf("8 | . | . | . | X | . | . | O | O | .\n");
    printf("  -------------------------------------\n\n");
    printf("El juego se gana cuando un jugador logre destruir todos los barcos del rival.\n");
    continuar();
}
int main(){//Menu principal
    int select = 0,flag = 0;
    do{
        printf("Battleship\n 1. Jugar\n 2. Instrucciones\n 3. Salir\n");
        scanf("%i",&select);
        switch(select){
        case 1:
            Inicio();//Inicia el juego
            printf("Volver al menu?\n 1. Si\n else. No\n");
            scanf("%i",&select);
            if(select < 1 || select > 1){
                flag = 1;
            }
            system("cls");
            break;
        case 2:
            Instrucciones();
            break;
        case 3://Cierra el programa
            flag = 1;
            break;
        default://Resetea el programa
            system("cls");
            printf("Seleccione una opcion valida\n");
            break;
        }
    }while(flag == 0);
    return 0;
}
