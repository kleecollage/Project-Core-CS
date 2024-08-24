import React, { useState } from 'react'
import { Divider, List, ListItem, ListItemText } from '@material-ui/core'    
import { Link } from 'react-router-dom'

export const MenuIzquierda = ({classes}) => (
    <div className={classes.list}>
        {/* ====================   PERFIL   ==================== */}
        <List>
            <ListItem component={ Link } button to="/auth/perfil">
                <i className='material-icons'>account_box</i>
                <ListItemText classes={{primary: classes.listItemText}} primary='Perfil'/>
            </ListItem>
            <Divider />
            {/* ====================   CURSOS   ==================== */}
            <List>
                <ListItem component={ Link } button to="/curso/nuevo">
                    <i className='material-icons'>add_box</i>
                    <ListItemText classes={{primary: classes.listItemText}} primary='Nuevo Curso'/>
                </ListItem>

                <ListItem component={ Link } button to="/curso/lista">
                    <i className='material-icons'>menu_book</i>
                    <ListItemText classes={{primary: classes.listItemText}} primary='Lista Cursos'/>
                </ListItem>
            </List>
            <Divider />
            {/* ====================   INSTRUCTORES   ==================== */}
            <List>
                <ListItem component={ Link } button to="/instructor/nuevo">
                    <i className='material-icons'>person_add</i>
                    <ListItemText classes={{primary: classes.listItemText}} primary='Nuevo Instructor'/>
                </ListItem>
                
                <ListItem component={Link} button to="/instructor/lista">
                    <i className='material-icons'>people</i>
                    <ListItemText classes={{primary: classes.listItemText}} primary='Lista Instrucotores'/>
                </ListItem>
            </List>

        </List>
    </div>
)