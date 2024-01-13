import React from 'react'
import Navbar from '../components/common/Navbar'
import Sidebar from '../components/common/Sidebar'

function MainLayout({children}) {
    return (
        <div>
            <Sidebar></Sidebar>
            <Navbar></Navbar>
            <div>{children}</div>
        </div>
    )
}

export default MainLayout