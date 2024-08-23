import './App.css';
import React from 'react';
import theme from './theme/theme';
import { ThemeProvider as MuiThemeProvider } from '@material-ui/core/styles';
import RegitrarUsuario from './componentes/seguridad/RegistrarUsuario.jsx';
import Login from './componentes/seguridad/Login.jsx';
import PerfilUsuario from './componentes/seguridad/PerfilUsuario.jsx';
import { Grid } from '@material-ui/core';
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"
import AppNavbar from './componentes/navegacion/AppNavbar.jsx';
function App() {
  
  return (
    <Router>
      <MuiThemeProvider theme={theme}>
        <AppNavbar/>
        <Grid container>
          <Switch>
            <Route exact path="/auth/login" component={ Login } />
            <Route exact path="/auth/registrar" component={ RegitrarUsuario } />
            <Route exact path="/auth/perfil" component={ PerfilUsuario } />
            <Route exact path="/" component={ PerfilUsuario } />
          </Switch>
        </Grid>
      </MuiThemeProvider>
    </Router>
  );
}

export default App;