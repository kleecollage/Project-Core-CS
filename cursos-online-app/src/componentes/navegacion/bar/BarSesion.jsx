import { Avatar, Button, IconButton, makeStyles, Toolbar, Typography } from '@material-ui/core'
import React from 'react'
import FotoUsuarioTemp from '../../../logo.jpg'

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
    }
}))

const BarSesion = () => {
    const classes = useStyles();
    return (
        <Toolbar>
            <IconButton color='inherit'>
                <i className='material-icons'>menu</i>
            </IconButton>

            <Typography variant='h6'>Cursos Online</Typography>

            <div className={classes.grow} />
            
            <div className={classes.seccionDesktop}>
                <Button color='inherit'>
                    Salir
                </Button>
                <Button color='inherit'>
                    {"Nombre de usuario"}
                </Button>
                <Avatar src={FotoUsuarioTemp} />
            </div>
        </Toolbar>
    );
}

export default BarSesion