import { Loader } from "@progress/kendo-react-indicators";
import { Card, CardBody } from "@progress/kendo-react-layout";
import Header from "./Components/Header/Header";
import StatisticsGrid from "./Components/Statistics/StatisticsGrid";
import ProfileInfo from "./Components/ProfileInformation/ProfileInfo";
import RecentActivity from "./Components/RecentActivity/RecentActivity";
import useGetInterviewSessionStatistics from "../../Hooks/InterviewSessions/useGetInterviewSessionStatistics";
import useGetRecentInterviewSessions from "../../Hooks/InterviewSessions/useGetRecentInterviewSessions";
import useGetProfilePictureUrl from "../../Hooks/Home/ProfilePicture/useGetProfilePicture";
import useGetUser from "../../Hooks/Home/User/useGetUser";
import type { InterviewSessionActivity, ProfileStats, User } from "../../Utils/interfaces";

const HomeContainer = () => {
  const { data: user, isLoading: isUserLoading, isError: isUserError } = useGetUser();
  const { data: profilePictureUrl, isLoading: isProfileLoading, isError: isProfileError } = useGetProfilePictureUrl();
  const { data: interviewSessionStatistics, isLoading: isInterviewSessionLoading, isError: isInterviewSessionError } = useGetInterviewSessionStatistics();
  const { data: recentActivities, isLoading: isRecentActivitiesLoading, isError: isRecentActivitiesError } = useGetRecentInterviewSessions();

  if (isUserLoading || isProfileLoading || isInterviewSessionLoading || isRecentActivitiesLoading) {
    return (
      <div className="min-h-[calc(100vh-4.05rem)] sm:min-h-[calc(100vh-4.55rem)] bg-background flex items-center justify-center">
        <div className="flex flex-col items-center space-y-4">
          <Loader size="large" />
          <span className="text-text-secondary">Loading your profile...</span>
        </div>
      </div>
    );
  }

  if (isUserError || isProfileError || isInterviewSessionError || isRecentActivitiesError) {
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
        <StatisticsGrid profileStats={interviewSessionStatistics as ProfileStats} />

        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div className="col-span-1">
            <ProfileInfo 
              user={user as User}
              profilePictureUrl={profilePictureUrl as string}
            />
          </div>

          <div className="md:col-span-2">
            <RecentActivity recentActivity={recentActivities as InterviewSessionActivity[]} />
          </div>
        </div>
      </div>
    </div>
  );
};

export default HomeContainer;
