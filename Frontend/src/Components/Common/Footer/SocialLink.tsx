interface SocialLinkProps {
  href: string;
  iconClassName: string;
}

const SocialLink = ({ href, iconClassName }: SocialLinkProps) => {
  return (
    <a href={href} target="_blank" className="text-text-tertiary hover:text-primary transition-colors duration-200">
      <i className={`${iconClassName}`} aria-hidden="true" />
    </a>
  );
};

export default SocialLink;