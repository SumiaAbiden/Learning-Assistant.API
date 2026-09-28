import { Link } from 'react-router-dom'

const menuItems = [
    { label: 'Dashboard', path: '/' },
    { label: 'Topics', path: '/topics' },
    { label: 'Journal', path: '/journal' },
]

function Sidebar() {
    return (
        <aside className="sidebar">
            <h2>Learning Assistant</h2>

            <nav>
                <ul>
                    {menuItems.map((item) => (
                        <li key={item.path}>
                            <Link to={item.path}>{item.label}</Link>
                        </li>
                    ))}
                </ul>
            </nav>
        </aside>
    )
}

export default Sidebar