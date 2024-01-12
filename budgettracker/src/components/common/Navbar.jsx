import React from 'react'
import { Link } from "react-router-dom";

function Navbar() {
    return (
        <div className='navbar'>
            <div className='navbar-logo'>
            </div>
            <ul className='navbar-menu'>
                <li><Link to="/">Home</Link></li>
                <li><Link to="/login">Login</Link></li>
                <li><Link to="/register">Register</Link></li>
            </ul>
        </div>
    )
}

export default Navbar