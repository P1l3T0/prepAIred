import StatisticsGrid from "./Components/Statistics/StatisticsGrid";
import ProfileInfo from "./Components/ProfileInformation/ProfileInfo";
import RecentActivity from "./Components/RecentActivity/RecentActivity";

const HomeContainer = () => {
  return (
    <main className="bg-background">
      <div className="mx-auto p-6">
        <StatisticsGrid />
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div className="col-span-1">
            <ProfileInfo />
          </div>
          <div className="md:col-span-2">
            <RecentActivity />
          </div>
        </div>
      </div>
    </main>
  );
};

export default HomeContainer;
