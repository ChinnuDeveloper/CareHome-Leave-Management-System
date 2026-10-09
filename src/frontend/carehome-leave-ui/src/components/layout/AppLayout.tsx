import { Outlet } from "react-router-dom";
import Sidebar from "./Sidebar";
import TopBar from "./TopBar";

export default function AppLayout(){
    return(
        <div className="app-layout">
            <Sidebar/>
            <div className="main">
                <TopBar/>

                <main className="content">
                    <Outlet/>
                </main>
            </div>
        </div>
    );
}