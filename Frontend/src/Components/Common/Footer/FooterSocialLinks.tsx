import SocialLink from "./SocialLink";

const FooterSocialLinks = () => {
  return (
    <div className="flex space-x-4">
      <SocialLink href="https://www.youtube.com/watch?v=dQw4w9WgXcQ" iconClassName="fa-brands fa-facebook" />
      <SocialLink href="https://www.youtube.com/watch?v=dQw4w9WgXcQ" iconClassName="fa-brands fa-twitter" />
      <SocialLink href="https://www.youtube.com/watch?v=dQw4w9WgXcQ" iconClassName="fa-brands fa-instagram" />
      <SocialLink href="https://www.youtube.com/watch?v=dQw4w9WgXcQ" iconClassName="fa-brands fa-github" />
    </div>
  );
};

export default FooterSocialLinks;
