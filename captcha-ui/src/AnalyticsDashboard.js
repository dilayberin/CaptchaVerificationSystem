import { useEffect, useState } from "react";

import {
    PieChart,
    Pie,
    Cell,
    Tooltip
} from "recharts";

function AnalyticsDashboard() {

    const [stats, setStats] = useState(null);

    useEffect(() => {

        loadStatistics();

    }, []);

    const loadStatistics = async () => {

        const response = await fetch(
            "http://localhost:5015/api/CaptchaAnalytics/statistics"
        );

        const data = await response.json();

        setStats(data);
    };

    if (!stats) {
        return <h1>Loading...</h1>;
    }

    const pieData = [
        { name: "Human", value: stats.humanCount },
        { name: "Suspicious", value: stats.suspiciousCount },
        { name: "Bot", value: stats.botCount }
    ];

    const COLORS = ["#10b981", "#f59e0b", "#ef4444"];

    return (

        <div
            style={{
                minHeight: "100vh",
                background: "#f3f4f6",
                padding: "40px",
                fontFamily: "Arial"
            }}
        >

            <h1
                style={{
                    textAlign: "center",
                    marginBottom: "40px"
                }}
            >
                Captcha Analytics Dashboard
            </h1>

            <div
                style={{
                    display: "grid",
                    gridTemplateColumns: "repeat(3, 1fr)",
                    gap: "20px",
                    marginBottom: "40px"
                }}
            >

                <StatCard
                    title="Total Attempts"
                    value={stats.totalAttempts}
                />

                <StatCard
                    title="Success Rate"
                    value={`${stats.successRate}%`}
                />

                <StatCard
                    title="Average Score"
                    value={stats.averageScore}
                />

                <StatCard
                    title="Average Response Time"
                    value={`${stats.averageResponseTimeMs} ms`}
                />

                <StatCard
                    title="Human Count"
                    value={stats.humanCount}
                />

                <StatCard
                    title="Bot Count"
                    value={stats.botCount}
                />

            </div>

            <div
                style={{
                    display: "flex",
                    justifyContent: "center"
                }}
            >

                <div
                    style={{
                        background: "white",
                        padding: "20px",
                        borderRadius: "20px",
                        boxShadow: "0 10px 30px rgba(0,0,0,0.1)"
                    }}
                >

                    <h2
                        style={{
                            textAlign: "center"
                        }}
                    >
                        Result Distribution
                    </h2>

                    <PieChart width={350} height={350}>

                        <Pie
                            data={pieData}
                            cx="50%"
                            cy="50%"
                            outerRadius={120}
                            dataKey="value"
                            label
                        >

                            {pieData.map((entry, index) => (

                                <Cell
                                    key={index}
                                    fill={COLORS[index]}
                                />

                            ))}

                        </Pie>

                        <Tooltip />

                    </PieChart>

                </div>

            </div>

        </div>
    );
}

function StatCard({ title, value }) {

    return (

        <div
            style={{
                background: "white",
                borderRadius: "20px",
                padding: "25px",
                boxShadow: "0 10px 20px rgba(0,0,0,0.08)",
                textAlign: "center"
            }}
        >

            <h3>{title}</h3>

            <h1
                style={{
                    marginTop: "15px",
                    color: "#4f46e5"
                }}
            >
                {value}
            </h1>

        </div>
    );
}

export default AnalyticsDashboard;