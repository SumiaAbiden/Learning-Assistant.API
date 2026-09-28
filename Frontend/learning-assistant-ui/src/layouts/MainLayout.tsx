import type { ReactNode } from 'react'
import Sidebar from '../components/layout/Sidebar'

interface MainLayoutProps {
    children: ReactNode
}

function MainLayout({ children }: MainLayoutProps) {
    return (
        <div className="app-layout">
            <Sidebar />

            <div className="main-content">
                <header>
                    <h1>Learning Assistant</h1>
                </header>

                <main>{children}</main>
            </div>
        </div>
    )
}

export default MainLayout