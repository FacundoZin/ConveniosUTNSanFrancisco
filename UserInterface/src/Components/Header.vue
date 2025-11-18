<template>
  <header class="main-header">
    <div class="header-content">
      <!-- Logo -->
      <router-link to="/">
        <img src="/Images/logoUTN.svg" alt="Logo Syntrax" class="logo" />
      </router-link>

      <!-- Links (a la derecha) -->
      <nav class="nav-links">
        <router-link to="/">Panel de búsqueda</router-link>
        <router-link to="/CargarConvenioMarco">Cargar Marco</router-link>
        <router-link to="/CargarConvenioEspecifico">Cargar Especifico</router-link>
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
  background-color: #f5f5f58d;
  background: linear-gradient(
    to right,
    #ffffff 0%,
    #f8f8f8 50%,
    #ececec 100%
  ); /* degradado sutil */
  backdrop-filter: blur(8px);
  -webkit-backdrop-filter: blur(8px);
  z-index: 1000;
  height: 70px;
  display: flex;
  align-items: center;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1); /* sombra inferior */
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

/* Logo */
.logo {
  height: 40px;
  object-fit: contain;
  display: block;
  transition:
    transform 0.3s ease,
    filter 0.25s ease;
}

.logo:hover {
  transform: scale(1.05); /* efecto zoom suave */
  filter: brightness(0) invert(0.4);
}

/* Links */
.nav-links {
  display: flex;
  gap: 2rem;
  align-items: center;
  margin-left: auto; /* empuja los links a la derecha */
}

.nav-links a {
  position: relative;
  color: rgb(69, 66, 66);
  text-decoration: none;
  font-weight: 500;
  font-size: 1rem;
  line-height: 1;
  transition: color 0.3s ease;
}

/* Efecto “barrita animada” debajo del link */
.nav-links a::after {
  content: '';
  position: absolute;
  bottom: -6px;
  left: 0;
  width: 0%;
  height: 2px;
  background: rgb(118, 113, 113);
  transition: width 0.3s ease;
}

.nav-links a:hover::after {
  width: 100%;
}

/* Link activo */
.nav-links a.router-link-active::after,
.nav-links a.router-link-exact-active::after {
  width: 100%;
  background: rgb(69, 66, 66);
}
</style>
