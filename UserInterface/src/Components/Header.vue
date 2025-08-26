<template>
  <header class="main-header">
    <div class="header-content">
      <!-- Logo -->
      <router-link to="/">
        <img src="/Images/logoUTN.svg" alt="Logo Syntrax" class="logo" />
      </router-link>

      <!-- Links -->
      <nav class="nav-links">
        <router-link to="/">Panel de búsqueda</router-link>
      </nav>
      <nav class="nav-links">
        <router-link to="/CargarConvenioMarco">Cargar Convenio Marco</router-link>
      </nav>
    </div>
  </header>
</template>

<style scoped>
.main-header {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  background-color: rgb(255, 255, 255); /* negro semitransparente al 50% */
  backdrop-filter: blur(8px); /* desenfoque sobre el fondo */
  -webkit-backdrop-filter: blur(8px); /* para compatibilidad */
  /*backdrop-filter: blur(4px);*/
  z-index: 1000;
  padding: 0; /* sin espacio extra */
  height: 70px; /* altura fija del header */
  display: flex;
  align-items: center; /* centra verticalmente el contenido */
}

.header-content {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 1rem;
  width: 100%;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

/* Logo reducido para no forzar altura */
.logo {
  height: 40px; /* más compacto */
  object-fit: contain;
  display: block;

  /*filter: brightness(0) invert(0);*/
  transition: filter 0.25s ease; /* animación suave */
}

.logo:hover {
  filter: brightness(0) invert(0.4);
  /* sigue siendo claro, pero un 20% menos brillante para ese efecto “gris” */
}

/* Links pegados al centro vertical del header */
.nav-links {
  display: flex;
  gap: 1rem;
  align-items: center;
}

.nav-links a {
  position: relative; /* Necesario para colocar la barrita debajo */
  color: rgb(69, 66, 66); /* Texto blanco por defecto */
  text-decoration: none; /* Sin subrayado */
  font-weight: 500;
  font-size: 1rem;
  line-height: 1;
  transition: color 0.3s ease;
}





/* Ruta activa EXACTA (Vue Router agrega esta clase automáticamente) → barra blanca fija */
.nav-links a.router-link-active::after,
.nav-links a.router-link-exact-active::after {
  background: rgb(118, 113, 113);
}


</style>