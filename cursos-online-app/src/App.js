import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"
import { ThemeProvider as MuiThemeProvider } from '@material-ui/core/styles';
import { Grid, Snackbar } from '@material-ui/core';
import './App.css';
import theme from './theme/theme';
import RegitrarUsuario from './componentes/seguridad/RegistrarUsuario.jsx';
import Login from './componentes/seguridad/Login.jsx';
import PerfilUsuario from './componentes/seguridad/PerfilUsuario.jsx';
import AppNavbar from './componentes/navegacion/AppNavbar.jsx';
import { useStateValue } from './contexto/store.js';
import { obtenerUsuarioActual } from './actions/UsuarioAction.js';

 function App() {
   const [{ openSnackbar}, dispatch] = useStateValue();
   const [iniciaApp, setiniciaApp] = useState(false);

   useEffect(() => {
     if (!iniciaApp) {
       obtenerUsuarioActual(dispatch)
        .then(response => {
          setiniciaApp(true)
         })
        .catch(error => {
          setiniciaApp(true)
         })
     }
   }, [iniciaApp]);

   return iniciaApp == false ? null : (
     <React.Fragment>  
       <Snackbar
         anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
         open={openSnackbar ? openSnackbar.open : false}
         autoHideDuration={2000}
         ContentProps={{ "aria-describedby": "message-id" }}
         message={
           <span id='message-id'>
             {openSnackbar ? openSnackbar.mensaje : ""}
           </span>
         }
         onClose={() => 
           dispatch({
             type: "OPEN_SNACKBAR",
             openMensaje: {
               open: false,
               mensaje: ""
             }
           })
         }
       />
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
    </React.Fragment>
  );
}

export default App;