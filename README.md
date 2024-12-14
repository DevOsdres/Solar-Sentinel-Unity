# Gradius-like Side-Scroller Shooter Game

Este proyecto es un **juego de disparos side-scroller** inspirado en la saga *Gradius*, desarrollado en Unity. El jugador controla una nave espacial y debe enfrentarse a oleadas de enemigos mientras avanza por niveles cada vez más desafiantes. El juego incluye una serie de mecánicas como power-ups, enemigos variados y un sistema de niveles.

## Instrucciones para ejecutar el proyecto

1. **Requisitos previos**:
   - [Unity 2021 o superior](https://unity.com/download) instalado en tu máquina.
   - Asegúrate de tener una versión de Unity con soporte para **WebGL** y **Windows**.

2. **Para ejecutar el proyecto localmente**:
   - Clona este repositorio a tu máquina:
     ```bash
     git clone https://github.com/TuUsuario/TuRepositorio.git
     ```
   - Abre el proyecto con **Unity**.
   - Si no tienes Unity configurado, puedes abrir el proyecto mediante Unity Hub.

3. **Ejecutar el build en Windows**:
   - Asegúrate de que el proyecto esté configurado para **Windows** en `File > Build Settings`.
   - Haz clic en **Build** para generar el archivo ejecutable del juego.

4. **Ejecutar el build en WebGL**:
   - Asegúrate de que el proyecto esté configurado para **WebGL** en `File > Build Settings`.
   - Haz clic en **Build** para generar la carpeta `Build` para WebGL y luego sube los archivos a una plataforma como **itch.io**.

5. **Controles**:
   - **Movimiento de la nave**: Usa las teclas **W, A, S, D** o las flechas direccionales para mover la nave.
   - **Disparar**: Presiona **Espacio** para disparar.
   - **Pausa**: Presiona **Esc** para pausar el juego.
   - **Salir**: Presiona el botón **Exit** en el menú para cerrar el juego.

---

## Gameplay y Diseño

### Jugabilidad
El juego sigue el formato clásico de un **shooter side-scroller** en el que el jugador controla una nave espacial que debe avanzar a través de niveles mientras combate oleadas de enemigos. El jugador tiene que esquivar los disparos enemigos, recoger power-ups y derrotar a los jefes de cada nivel para avanzar.

- **Nave y Disparos**:
  El jugador controla una nave con un sistema de disparos de alta velocidad similar a *Gradius*. La nave puede obtener power-ups que mejoran sus armas y habilidades.
  
- **Enemigos y Jefes**:
  En el camino, el jugador se enfrentará a una variedad de enemigos con patrones de movimiento predecibles y otros impredecibles. Al final de cada nivel, se enfrentará a un **jefe** con habilidades únicas.

- **Niveles**:
  Cada nivel tiene un diseño lineal donde el jugador avanza hacia la derecha mientras se enfrenta a enemigos y obstáculos.

### Interfaces de Usuario (UI)
El sistema de interfaces de usuario está diseñado para ser intuitivo y proporcionar toda la información relevante durante el juego.

- **HUD**:
  - **Vida del jugador**: Se muestra mediante íconos en la parte superior izquierda de la pantalla. El jugador tiene un número limitado de vidas.
  - **Puntaje**: El puntaje del jugador se muestra en la parte superior central de la pantalla.
  - **Nivel**: La información sobre el nivel actual se muestra en la parte superior derecha.
  - **Arma activa**: El HUD también muestra el nombre del arma que el jugador está usando actualmente.

- **Menú Principal**:
  El menú principal se presenta al inicio del juego y permite al jugador iniciar una nueva partida, cargar un nivel o salir.

- **Pantalla de Victoria**:
  Al completar el juego, el jugador es llevado a una pantalla de victoria con un mensaje que celebra el logro y ofrece la opción de regresar al menú principal.

### Diseño Visual
- **Escenarios**: Los niveles son visualmente variados, con fondos de parallax que proporcionan una sensación de profundidad. Los fondos cambian en cada nivel, haciendo que el jugador se sienta como si estuviera avanzando a través de diferentes entornos.
  
- **Enemigos**: Los enemigos tienen diseños únicos para cada nivel, con naves que se mueven de manera diferente según su comportamiento.

- **Efectos Especiales**: El juego tiene efectos visuales para los disparos, explosiones y power-ups, todos diseñados para mejorar la experiencia visual sin abrumar al jugador.

### Diseño de Power-ups
Los power-ups están diseñados para mejorar las capacidades del jugador. Existen varias categorías de power-ups que se pueden recoger durante el juego:

- **Mejoras de arma**: Aumentan la capacidad de disparo o introducen nuevas formas de disparar.
- **Escudo**: Proporciona protección temporal contra los disparos enemigos.
- **Velocidad**: Aumenta la velocidad de la nave temporalmente, permitiendo al jugador moverse más rápido.

---

## Contribuciones
Si deseas contribuir a este proyecto, siéntete libre de hacer un fork del repositorio y enviar tus mejoras mediante un pull request. Asegúrate de seguir las mejores prácticas de desarrollo y prueba tus cambios antes de enviarlos.

## Licencia
Este proyecto está bajo la licencia MIT - consulta el archivo [LICENSE](LICENSE) para más detalles.

---

¡Gracias por jugar! 🚀