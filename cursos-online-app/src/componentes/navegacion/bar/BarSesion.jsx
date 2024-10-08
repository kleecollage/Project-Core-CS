import React, { useState } from 'react'
import { withRouter } from 'react-router-dom'
import { Avatar, Button, Drawer, IconButton, makeStyles, Toolbar, Typography } from '@material-ui/core'
import FotoUsuarioTemp from '../../../logo.jpg'
import { useStateValue } from '../../../contexto/store'
import { MenuIzquierda } from './MenuIzquierda'
import { MenuDerecha } from './MenuDerecha'

const useStyles = makeStyles((theme) => ({
    seccionDesktop: {
        display: "none",
        [theme.breakpoints.up("md")]: {
            display: "flex"
        }
    },
    seccionMovil: {
        display: "flex",
        [theme.breakpoints.up("md")]: {
            display: "none"
        }
    },
    grow: {
        flexGrow: 1
    },
    avatarSize: {
        width: 40,
        height: 40
    },
    list: {
        width: 250,
    },
    listItemText: {
        fontSize: "14px",
        fontWeight: 600,
        paddingLeft: "15px",
        color: "#212121"
    }
}))

const BarSesion = (props) => {
    const classes = useStyles();
    const [{ sesionUsuario }, dispatch] = useStateValue()
    // HANDLE LEFT SIDE NAVBAR
    const [abrirMenuIzquierda, setAbrirMenuIzquierda] = useState(false)
    const cerrarMenuIzquierda = () => {
        setAbrirMenuIzquierda(false)
    }
    const abrirMenuIzquierdaAction = () => {
        setAbrirMenuIzquierda(true)
    }
    // HANDLE RIGTH SIDE NAVBAR
    const [abrirMenuDerecha, setAbrirMenuDerecha] = useState(false)
    const cerrarMenuDerecha = () => {
        setAbrirMenuDerecha(false)
    };
    const abrirMenuDerechaAction = () => {
        setAbrirMenuDerecha(true)
    }
    const salirSesionApp = () => {
        localStorage.removeItem('token_seguridad');
        dispatch({
            type: "SALIR_SESION",
            nuevoUsuario: null,
            autenticado: false,
        })
        props.history.push('/auth/login')
    }
    
    
    return (
        <React.Fragment>    
            <Drawer
                open={abrirMenuIzquierda}
                onClose={cerrarMenuIzquierda}
                anchor='left'
            >
                <div onKeyDown={cerrarMenuIzquierda} onClick={cerrarMenuIzquierda}>
                    <MenuIzquierda classes={classes}/>
                </div>
            </Drawer>

            <Drawer
                open={abrirMenuDerecha}
                onClose={cerrarMenuDerecha}
                anchor='right'
            >
                <div role='button' onClick={cerrarMenuDerecha} onKeyDown={cerrarMenuDerecha}>
                    <MenuDerecha
                        classes={classes}
                        salirSesion={salirSesionApp}
                        usuario = {sesionUsuario ? sesionUsuario.usuario : null}
                    />
                </div>

            </Drawer>

            <Toolbar>
                <IconButton color='inherit' onClick={abrirMenuIzquierdaAction}>
                    <i className='material-icons'>menu</i>
                </IconButton>

                <Typography variant='h6'>Cursos Online</Typography>

                <div className={classes.grow} />
                
                <div className={classes.seccionDesktop}>
                    <Button color='inherit'>
                        Salir
                    </Button>
                    <Button color='inherit'>
                        {sesionUsuario ?  sesionUsuario.usuario.nombreCompleto : "Unauthenticathed"}
                    </Button>
                    <Avatar src={sesionUsuario.usuario.imagenPerfil || FotoUsuarioTemp} />
                </div>

                <div className={classes.seccionMovil}>
                    <IconButton color='inherit' onClick={abrirMenuDerechaAction}>
                        <i className='material-icons'>more_vert</i>
                    </IconButton>
                </div>
                
            </Toolbar>
        </React.Fragment>
    );
}

export default withRouter(BarSesion);