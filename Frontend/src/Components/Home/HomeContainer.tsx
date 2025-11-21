import { Loader } from "@progress/kendo-react-indicators";
import { Card, CardBody } from "@progress/kendo-react-layout";
import useGetUser from "../../Hooks/User/useGetUser";
import type { Activity, ProfileStats, User } from "../../Utils/interfaces";
import Header from "./Components/Header/Header";
import StatisticsGrid from "./Components/Statistics/StatisticsGrid";
import ProfileInfo from "./Components/ProfileInformation/ProfileInfo";
import RecentActivity from "./Components/RecentActivity/RecentActivity";
import useGetProfilePictureUrl from "../../Hooks/ProfilePicture/useGetProfilePicture";

const mockProfileStats: ProfileStats = {
  totalInterviews: 23,
  passedInterviews: 18,
  ongoingInterviews: 2,
  averageScore: 87.5,
  completionRate: 78.3,
};

const mockRecentActivity: Activity[] = [
  {
    type: "Technical Interview",
    subject: "React Hooks",
    score: 92,
    date: "2024-11-12",
  },
  {
    type: "HR Interview",
    subject: "Communication Skills",
    score: 67,
    date: "2024-11-10",
  },
  {
    type: "Technical Interview",
    subject: "TypeScript",
    score: 46,
    date: "2024-11-08",
  },
  {
    type: "Technical Interview",
    subject: "TypeScript",
    score: 32,
    date: "2024-11-08",
  }
]

const HomeContainer = () => {
  const { data: user, isLoading: isUserLoading, isError: isUserError } = useGetUser();
  const { data: profilePictureUrl, isLoading: isProfileLoading, isError: isProfileError } = useGetProfilePictureUrl();

  if (isUserLoading || isProfileLoading) {
    return (
      <div className="min-h-[calc(100vh-4.05rem)] sm:min-h-[calc(100vh-4.55rem)] bg-background flex items-center justify-center">
        <div className="flex flex-col items-center space-y-4">
          <Loader size="large" />
          <span className="text-text-secondary">Loading your profile...</span>
        </div>
      </div>
    );
  }

  if (isUserError || isProfileError) {
    return (
      <div className="min-h-[calc(100vh-4.05rem)] sm:min-h-[calc(100vh-4.55rem)] bg-background flex items-center justify-center">
        <Card className="shadow-lg">
          <CardBody>
            <div className="text-center p-8">
              <h2 className="text-xl text-text-primary font-semibold mb-2">
                Unable to load profile
              </h2>
              <p className="text-text-secondary">
                Please try refreshing the page
              </p>
            </div>
          </CardBody>
        </Card>
      </div>
    );
  }

  return (
    <div className="min-h-[calc(100vh-4.55rem)] bg-background">
      <Header username={user?.username!} />

      <div className="max-w-7xl mx-auto p-6">
        <StatisticsGrid profileStats={mockProfileStats} />

        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div className="col-span-1">
            <ProfileInfo
              user={user as User}
              profilePictureUrl={profilePictureUrl || ""}
            />
          </div>

          <div className="md:col-span-2">
            <RecentActivity recentActivity={mockRecentActivity} />
          </div>
        </div>
      </div>
    </div>
  );
};

export default HomeContainer;
