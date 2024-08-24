import React from 'react'
import { Link } from 'react-router-dom/'
import { Avatar, List, ListItem, ListItemText } from '@material-ui/core'
import FotoUsuarioTemp from '../../../logo.jpg'

export const MenuDerecha = ({
    classes,
    usuario,
    salirSesion
}) => (
    <div className={classes.list}>
        <List>
            <ListItem button component={Link}>
                <Avatar src={usuario.foto || FotoUsuarioTemp} />
                <ListItemText classes={{primary: classes.listItemText}} primary={ usuario ? usuario.nombreCompleto : ''} />
            </ListItem>
            <ListItem button onClick={salirSesion}>
                <ListItemText classes={{primary: classes.listItemText}} primary="Salir"/>
            </ListItem>
        </List>

    </div>
)
